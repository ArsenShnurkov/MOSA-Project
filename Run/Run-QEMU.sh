#!/bin/bash
mono ../bin/Mosa.Tool.CreateBootImage.exe IMG/mosaboot-img.config build/bootimage.img
/usr/bin/qemu-system-x86_64 -m 512M -cpu qemu32 -hda build/bootimage.img
