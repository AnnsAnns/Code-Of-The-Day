use image::{ImageBuffer, Rgb};
use num_complex::Complex;
use hsl::HSL;

#[derive(Debug)]
struct Fractal {
    buf: ImageBuffer<Rgb<u8>, Vec<u8>>,
    ppu: f32
}

type C = Complex<f32>;

impl Fractal {

    pub fn new(width: u32, height: u32, multiplier: f32) -> Self {
        Self {
            buf: Self::get_rainbow_buf(width, height, multiplier),
            ppu: 256.0
        }
    }
    
    fn get_rainbow_buf(width: u32, height: u32, multiplier: f32) -> ImageBuffer<Rgb<u8>, Vec<u8>> {
        let mut buf = image::ImageBuffer::new(width, height);

        // use rayon to iter over buf pixels in parallel
        // let b: () = &mut *buf;

        for (x, y, pixel) in buf.enumerate_pixels_mut() {
            let r = (multiplier * x as f32) as u8;
            let b = (multiplier * y as f32) as u8;
            *pixel = Rgb([r, 0, b]);
        }

        buf
    }

    pub fn create_newton_fractals(&mut self) -> &mut Self {
        let (width, height, ppu) = (self.width(), self.height(), self.ppu);
        for (x, y, p) in self.buf.enumerate_pixels_mut() {
            *p = Self::coords_to_newton_pixel(x, y, width, height, ppu);
        }
        self
    }

    fn newtons_algo<F, Fd>(x0: C, f: &F, fd: &Fd, n: u32) -> C
    where F:  Fn(C) -> C,
            Fd: Fn(C) -> C
    {
        let mut x = x0;
        for _ in 0..n {
            x = x - (f(x) / fd(x));
        }
        x
    }

    #[inline(always)]
    fn pixel_to_num(x: u32, y: u32, width: u32, height: u32, ppu: f32) -> C {
        Complex::new(0.0, 0.0) + Complex::new(
            (x as f32 - 0.5 * width as f32) / ppu,
            (y as f32 - 0.5 * height as f32) / ppu
        )
    }

    fn num_to_colour(x: C) -> Rgb<u8> {
        let h = HSL { h: ((x.re / x.im).atan() * (180.0 / core::f32::consts::PI)) as f64, s: 0.75, l: 0.5 };

        let (r, g, b) = h.to_rgb();

        //println!("{:?} ==> {:?} ==> RGB ({:?}, {:?}, {:?})", x, h, r, g, b);

        Rgb([r, g, b])
    }

    fn coords_to_newton_pixel(x: u32, y: u32, width: u32, height: u32, ppu: f32) -> Rgb<u8> {
        Self::num_to_colour(
            Self::newtons_algo(
                Self::pixel_to_num(x, y, width, height, ppu), 
                &|x| x * x * x - C::new(1.0, 0.0), 
                &|x| C::new(3.0, 0.0) * x * x, 
                10
            )
        )
    }
}

impl core::ops::Deref for Fractal {
    type Target = ImageBuffer<Rgb<u8>, Vec<u8>>;

    fn deref(&self) -> &Self::Target { &self.buf }
}


fn main() {
    Fractal::new(20000, 20000, 0.3)
        .create_newton_fractals()
        .save("fractal.png")
        .unwrap();
}
