use rocket::data::{Data, FromData, Outcome, ToByteUnit};
use rocket::http::Status;
use rocket::http::uri::error::PathError;
use rocket::request::{Request, FromParam};
use rand::{self, Rng};
use std::error::Error;
use std::fmt::{self, Debug, Display};
use std::{fs, io, path::{Path, PathBuf}};

pub struct Pasta(String);

impl Pasta {
    pub fn store_to(self, path: Option<PastaPath>) -> io::Result<String> {
        match path {
            Some(path) => path,
            None => PastaPath::new_unique()
        }.write(self.0)
    }

    pub fn get(pasta: PastaPath) -> io::Result<String> {
        pasta.retrieve().map(|s| s.0)
    }
}

#[rocket::async_trait]
impl<'r> FromData<'r> for Pasta {
    type Error = &'static str;

    async fn from_data(req: &'r Request<'_>, data: Data<'r>) -> Outcome<'r, Self> {
        let text = match data
            .open(req.limits().get("pasta").unwrap_or(1_337_420.bytes()))
            .into_string()
            .await
        {
            Ok(text) if text.is_complete() => text.into_inner(),
            Ok(_) => return Outcome::Failure((Status::PayloadTooLarge, "Payload too large")),
            Err(_e) => {
                return Outcome::Failure((Status::InternalServerError, "yeah idk, shit happened"))
            }
        };

        if text.to_lowercase().contains("pasta") {
            return Outcome::Success(Pasta(text));
        }

        Outcome::Failure((Status::Forbidden, "WE ONLY ALLOW PASTAS HERE"))
    }
}

pub struct PastaPath(String);

impl PastaPath {
    pub const PASTA_DIR: &'static str = "./pastas/";
    pub const PASTA_PREFIX: &'static str = "pasta_";
    const INITIAL_LETTERS: usize = 7;
    const BASE62_CHARS: &'static [u8] = b"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    fn new_unique() -> Self {
        let mut ctr = 0;
        let path = loop {
            let path = PastaPath::generate(ctr / 10);

            if !PathBuf::from(&path).exists() {
                break path;
            }

            ctr += 1;
        };
        
        PastaPath(path)
    }

    fn generate(additional_letters: usize) -> String {
        let mut buf = String::with_capacity(Self::INITIAL_LETTERS + additional_letters + Self::PASTA_PREFIX.len());
        buf.push_str(Self::PASTA_PREFIX);
        let mut rng = rand::thread_rng();
        for _ in 0..(Self::INITIAL_LETTERS + additional_letters) {
            buf.push(
                Self::BASE62_CHARS[rng.gen::<usize>() % 62] as char
            );
        }
        buf
    }

    fn as_path(&self) -> PathBuf {
        PathBuf::from(Self::PASTA_DIR).join(&self.0)
    }

    fn write(self, data: String) -> io::Result<String> {
        fs::write(self.as_path(), data)?;
        Ok(self.0)
    }

    pub fn retrieve(&self) -> io::Result<Pasta> {
        Ok(Pasta(fs::read_to_string(self.as_path())?))
    }
}

#[derive(Debug)]
pub enum PastaPathError {
    PathError(PathError),
    NonExistantPasta(PathBuf),
    FuckOffWithTraversalAttacks,
    CouldntConvertToUtf8,
}

impl Display for PastaPathError {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        <PastaPathError as Debug>::fmt(self, f)
    }
}

impl Error for PastaPathError {}

impl From<PathError> for PastaPathError {
    fn from(e: PathError) -> PastaPathError {
        PastaPathError::PathError(e)
    }
}

impl<'r> FromParam<'r> for PastaPath {
    type Error = PastaPathError;

    fn from_param(param: &'r str) -> Result<Self, Self::Error> {
        let pathbuf = PathBuf::from_param(param)?;

        if !PathBuf::from(PastaPath::PASTA_DIR).join(&pathbuf).is_file() {
            return Err(Self::Error::NonExistantPasta(pathbuf));
        }

        if pathbuf == Path::new("..") {
            return Err(Self::Error::FuckOffWithTraversalAttacks);
        }

        match pathbuf.to_str() {
            Some(path) => Ok(PastaPath(path.to_owned())),
            _ => Err(Self::Error::CouldntConvertToUtf8),
        }
    }
}
