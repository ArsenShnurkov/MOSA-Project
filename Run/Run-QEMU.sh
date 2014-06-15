#!/bin/bash
mono ../bin/Mosa.Tool.CreateBootImage.exe IMG/mosaboot-img.config build/bootimage.img
/usr/bin/qemu -m 512M  -hda build/bootimage.img
