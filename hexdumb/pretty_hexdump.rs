unsafe fn memdump(addr: usize, len: usize, step: usize) {
    // UNSAFE: [addr;addr+len] must be valid addresses to dereference
	
	/* make sure step can't cause this to panic */
    let step = if step == 0 { 16 } else { step };
	
    let header_offsets = (0..step)
        .enumerate()
        .map(|(idx, num)| {
            format!(
                "{:02x?}{}",
                num,
                if (idx + 1) % 4 == 0 && idx != step {
                    "  "
                } else {
                    " "
                }
            )
        })
        .collect::<Vec<String>>()
        .join("");
    let delim_str = "-".repeat(header_offsets.len());
    println!("|----------------|-{}-|", delim_str);
    println!("|    Offset      |  {}|", header_offsets);
    println!("|----------------|-{}-|", delim_str);
    for off in (addr..addr + len).step_by(step) {
        print!("| 0x{:012x?} |  ", off);
        for i in 0..step {
            if off + i >= addr + len {
                print!("  {}", if (i + 1) % 4 == 0 { "  " } else { " " });
            } else {
                print!(
                    "{:02x?}{}",
                    std::ptr::read_unaligned((off + i) as *const u8),
                    if (i + 1) % 4 == 0 { "  " } else { " " }
                );
            }
        }
        print!("| \n");
    }
    println!("|----------------|-{}-|", delim_str);
}

fn main() {
    unsafe {
        memdump(main as usize, 0x12, 0);
    }
}
