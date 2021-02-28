#!/bin/bash
# Marek Machliński 241308

# a)
echo "Open files count: $(lsof -Fn | cut -d'n' -f2- | tr " " "\n" | grep "^/" | uniq | wc -l)"

# b)
lsof -Fnst0 | grep -a "tREG" | cut -d's' -f2- | sort -rn 2>&1 | uniq | head | tr "\0" " "

# c)
lsof | tr -s " "

# d)
current_pid=-1
lsof -Fnt0 | uniq | grep -a "^p\|tREG" | grep -a -v "DEL" | cut -d'n' -f2- | while read -r var
do
    if [[ "$var" == p* ]]; then
        if ! [[ -z ${pid_map[$current_pid]} ]]; then
            if [ "${var:1}" != "$current_pid" ]; then
                echo PID: "$current_pid", open files: ${pid_map[$current_pid]}
            fi
        fi
        current_pid="${var:1}"
        pid_map["$current_pid"]=0
    else
        ((pid_map["$current_pid"]++))
    fi
done