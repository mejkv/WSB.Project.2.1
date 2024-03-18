#!/bin/bash

declare -A commands
commands=(-u="dotnet ConsoleCommandExecutor.dll -u"
          -h="dotnet ConsoleCommandExecutor.dll -h")

cmd=$1
shift

if [[ -n "${commands[$cmd]}" ]]; then
    echo "Wywołuję: ${commands[$cmd]} $@"
    ${commands[$cmd]} $@
else
    echo "Nieznana komenda: $cmd"
    exit 1
fi
