#!/bin/bash
# Marek Machliński 241308

if [ -z "$1" ]; then
    echo "No file specified!"
    exit 1
fi
if [ ${1: -4} != "json" ]; then
    echo "File is not a json!"
    exit 2
fi

outfile=${1: 0:-4}"xml"

cat $1 | awk 'BEGIN { print "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"; print "<root>"; RS = "{|,"; FS = ":"; indx = -1 } \
    END { printf "</root>" "" } \
    NF != 0 \
    { \
        gsub(" \"","\""); \
        gsub("\" ","\""); \
        current_key = substr($1, 2, length($1)-2); \
        if ($2 ~ /"*"/) \
        { \
            is_struct_end = "false"; \
            current_val = $2 ; \
            if (current_val ~ /}/)
            { \
                is_struct_end = "true"; \
                gsub("}",""); \
                gsub("\" ","\""); \
                current_val = $2; \
            } \
            if (length(current_val) > 2) \
            { \
                current_val = substr(current_val, 2, length(current_val)-2); \
                printf "<"current_key">" current_val; \
                print "</"current_key">"; \
            } \
            else \
                print "<"current_key" />"; \
            if (is_struct_end == "true") \
            { \
                print "</"current_struct[indx]">"; \
                --indx; \
                is_struct_end = "false"; \
            } \
        } \
        else if ($2 ~ /null/) \
            print "<"current_key" null=\"true\" />"; \
        else if (length($2) <= 1) \
        { \
            print "<"current_key">"; \
            ++indx; \
            current_struct[indx] = current_key; \
        } \
        else \
        { \
            printf "<"current_key">" substr($2, 2, length($2)-1); \
            print "</"current_key">"; \
        } \
    }' > "$outfile"