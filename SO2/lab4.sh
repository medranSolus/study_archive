#!/bin/bash
# Marek Machliński 241308

if [ -z "$1" ]; then
    echo "No catalog specified!"
    exit -1
fi
if ! [[ -r "$1" ]]; then
    echo "Cannot read catalog: $1"
    exit -2
fi
if [ -z "$2" ]; then
    echo "No user specified!"
    exit -3
fi

sudo -u "$2" find "$1" -executable -type f