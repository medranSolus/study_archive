#!/bin/bash
# Marek Machliński 241308

lsusb | awk '$0 !~ /root hub/ { for (i=6; i<=NF; ++i) printf $i " "; print " " }'