use rocket::{self, http::Status};
use crate::Pasta;

#[rocket::post("/paste", data = "<pasta>")]
pub fn paste(pasta: Pasta) -> Result<String, Status> {
    match pasta.store_to(None) {
        Ok(name) => Ok(name),
        Err(_) => Err(Status::InternalServerError),
    }
}
