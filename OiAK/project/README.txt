|--------------------------------------------------------------------------|
|---------------- Programs invoke methods and descriptions ----------------|
|--------------------------------------------------------------------------|


Test generator
    Description:
        Calls test collector for every negation program with every mode type and with height and width values in [100, 200, 500, 1000, 2000, 5000, 10000].
    Invoke:
        testGen

Test collector
    Description:
        Call 100 times program with specified input parameters and creates file with average time execution of that programs in micro seconds.
    Output file name:
        program_type = 0:
            <width>x<height>_seq_test.bmp_testTime.txt
        program_type != 0:
            row_mode = 0:
                <width>x<height>_par_one_test.bmp_testTime.txt
            row_mode != 0:
                <width>x<height>_par_max_test.bmp_testTime.txt
    Output file format: 
        <time_in_micro_seconds> <width> <height>
    Invoke:
        testCol <width> <height> <bitmap_name> [<program_type> = 0] [<parallel_row_mode> = 1] [<parallel_memory_mode> = 0]
    Params:
        <width>, <height> - size of bitmap (positive values)
        <bitmap_name> - path to bitmap
        <program_type> - 0: sequential version of program | <any other value>: parallel version of program
        <parallel_row_mode> - 0: one row per subprogram | <any other value>: maximal number of rows per subprogram according to number of cores
        <parallel_memory_mode> - 0: every subprogram (negParRowLoad) loads bitmap to memory | <any other value>: subprograms (negParRow) use same shared bitmap memory

Sequential negation program
    Description:
        Creates negative image of file specified in input parameter. Creates file with execution time in micro seconds.
    Output file name:
        NEG_<bitmap_name>
        _imageTime.txt
    Output file format (_imageTime.txt): 
        <time_in_micro_seconds> <width> <height>
    Invoke:
        negSeq <bitmap_name>
    Params:
        <bitmap_name> - path to bitmap

Parallel negation program
    Description:
        Creates negative image of file specified in input parameter calling subprogram negParRow (<memory_mode> != 0) or negParRowLoad (<memory_mode> = 0) for every row (<row_mode> != 0) or just one subprogram for every core with list of rows to negate. 
        Creates file with execution time in micro seconds.
        memory_mode != 0:
            While using negParRow program creates shared memory with bitmap structure in it.
        memory_mode = 0:
            Program reads row data created by negParRowLoad and merge it into one bitmap file.
    Output file name:
        NEG_<bitmap_name>
        _imageTime.txt
    Output file format (_imageTime.txt): 
        <time_in_micro_seconds> <width> <height>
    Invoke:
        negPar <bitmap_name> [<row_mode> = 1] [<memory_mode> = 0]
    Params:
        <row_mode> - 0: one row per subprogram | <any other value>: maximal number of rows per subprogram according to number of cores
        <memory_mode> - 0: every subprogram (negParRowLoad) loads bitmap to memory | <any other value>: subprograms (negParRow) use same shared bitmap memory

Parallel negation subprograms
    Shared memory mode
        Description:
            Negates rows specified on input parameters on shared memory created by negPar.
        Invoke: (cannot open from shell because first param is not a program name but bitmap path, needs parent to create shared memory)
            |negParRow| <bitmap_name> <row_number> [row_count = 1]
        Params:
            <bitmap_name> - path to bitmap
            <row_number> - number of row in bitmap to create negation
            <row_count> - number of rows starting from <row_number> to create negation
    
    Classic bitmap load mode
        Description:
            Loads bitmap and negates rows specified on input parameters. Then every row is written to a file contatining binary data of that row after negation.
        Output file name:
            _row_<row_number>_NEG_<bitmap_name>
        Invoke: (cannot open from shell because first param is not a program name but bitmap path)
           |negParRowLoad| <bitmap_name> <row_number> [row_count = 1]
        Params:
            <bitmap_name> - path to bitmap
            <row_number> - number of row in bitmap to create negation
            <row_count> - number of rows starting from <row_number> to create negation
     