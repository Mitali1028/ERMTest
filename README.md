# ERMTest

This is the solution for processing LP and TOU files as requested in task description.

## Design consideration.

During design of the solution I have considered future change requests like. 
 1. Requirement of processing new file type (text file, db etc) and files type that is not LP or TOU.
 2. Different type of processing requirement for existing file. Like calcuate mean or change column required to be processed.
 3. Requirement to change filteration criteria.
 4. Requirement to change parameters printed in output or change the output from console to file or database etc.


## Solution

1. To make filePath configurable - i am using command line argument to read filePath.
2. Column required to be processed for median calculation and the percentage threshold for filter are configured in the config file for now. This
   can be candidate for command line argument. 
3. Following is the Structure of the code :
   * All interfaces are generic so they are extensible for other types of file.
   * IParser - interface to parse any filePath (other type of file can implements it)
   * CSVParser implements the parser and read file and create dataObject 
   * IProcess : interface to process the file 
   * CSVProcess : implements the IProcess and find median , filter the data and send to output class to print
   * IFilter : interface to filter data. 
   * DeviationPercentage : filter on percentage.
   * Ioutput - inteface for output (can be extensible for other types of output)
   * Output - class to print output on console.
   * Helper - static function to calculate median and extract fileName.
4. Unit test project added to test functionality. Not all functions have been tested but enough code coverage has been added to demonstrate the intent.


## Things considered but not taken care right now.

1. Size of file. What if the whole file cannot be read in memory.

## How to execute.

1. Repository has Debug and Release folder with ERMTest.exe
2. When you run the exe it will ask for path of file to be processed.
3. If the file has output it will print the output and end. If it has not output it currently just ends and does not provide feedback. It can be improved.
4. If you want to change the precentage used for filtering, edit ERMTest.exe.config file and change the value for related filetype.

## Feedback
It would be great if you can review the code and provide any feedback on architecture and coding based on your experience. 



  
   