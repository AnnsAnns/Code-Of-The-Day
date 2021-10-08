use rocket;
use crate::{Pasta, PastaPath};
use std::io;

#[rocket::get("/<path>")]
pub fn get(path: PastaPath) -> io::Result<String> {
    Pasta::get(path)
}
