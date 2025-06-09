# Excel Processing Azure Function

This project contains an Azure Function that parses an Excel file and stores the contents in the existing `LibraryApi` database.

The HTTP triggered function `ProcessExcel` expects the request body to contain an Excel workbook. The first worksheet should have the columns:

1. **Author** – the author's name
2. **Title** – the book title

For each row, the function creates the author if it does not already exist and then adds the book associated with that author.

## Usage

Deploy the function to Azure Functions or run it locally. Send a `POST` request to the function URL with the Excel file as the request body.
