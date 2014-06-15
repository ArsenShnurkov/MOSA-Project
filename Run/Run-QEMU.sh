#!/bin/bash
mono ../bin/Mosa.Tool.CreateBootImage.exe IMG/mosaboot-img.config build/bootimage.img
/usr/bin/qemu-system-i386 -m 512M -hda build/bootimage.img
