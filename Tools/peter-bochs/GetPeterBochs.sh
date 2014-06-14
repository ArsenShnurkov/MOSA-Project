#!/bin/bash

FILENAME=peter-bochs-debugger20120606.jar
FILENAME_UNVERSIONED=peter-bochs-debugger.jar
WGET=`which wget`

if [ ! -f ${FILENAME} ]; then
    ${WGET} -c http://peter-bochs.googlecode.com/files/${FILENAME}
fi

if [ ! -f ${FILENAME} ]; then
    ${WGET} -c http://download.mosa-project.org/${FILENAME}
fi

if [ -f ${FILENAME} ]; then
  if [ ! -f ${FILENAME_UNVERSIONED} ]; then
    ln -s ${FILENAME} ${FILENAME_UNVERSIONED}
    echo "Link was created"
  else
    echo "Link exists"
  fi
fi
