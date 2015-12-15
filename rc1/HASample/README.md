## High Availability Example

### Example Case

 - We have HAProxy instance which is exposed through localhost:5040 and pointing to localhost:6040.
 - We have a server application running and exposed through localhost:6040.
 - We have a client application running and calling the server on localhost:5040, outputing the console.
 - We will:
     - deploy a new version of the server application and expose it on localhost:6041
	 - flip the switch on HAProxy to point from localhost:5040 to localhost:6041
	 
   During this period, there should be no downtime on the client application.