use rocket;

pub mod api;
mod pasta;
pub use pasta::{Pasta, PastaPath};

use std::{fs, io};

#[rocket::get("/")]
fn index() -> &'static str {
    "Welcome to pastabin - your one time stopp for all things pasted pasta."
}

#[rocket::launch]
fn entry() -> _ {
    match fs::create_dir_all(PastaPath::PASTA_DIR) {
        Ok(()) => (),
        Err(ref e) if e.kind() == io::ErrorKind::AlreadyExists => (),
        Err(e) => panic!("{:?}", e),
    };

    rocket::build()
        .mount("/", rocket::routes![index])
        .mount("/api/", rocket::routes![api::paste, api::get])
        .mount("/pasta/", rocket::routes![api::get])
}
