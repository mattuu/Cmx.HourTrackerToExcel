<<<<<<< b3a6606e7c2ea4a6bd9db8f28fe272c41de02e04
# HourTracker to Excel Integrator

## API testing
``` bash
curl -X POST http://localhost:41006/api/file -F "formFiles=@CSVExport-Jet2com.csv" -H "Content-Type: multipart/form-data"
```
=======
# HourTracker to Excel Integrator

## API testing
``` bash
curl -X POST http://localhost:41006/file -F "formFiles=@CSVExport-Jet2com.csv" -H "Content-Type: multipart/form-data"
```
>>>>>>> Progress on FileController;
