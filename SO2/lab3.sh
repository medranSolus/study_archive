#!/bin/bash
# Marek Machliński 241308

if [ -z "$1" ]; then
    echo "No catalog specified!"
    exit -1
fi

rm $(find $1 -xtype l)