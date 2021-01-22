# Clean Code Wheel Of Fortune

# Technical requirements maded:
• C#. • Dotnet Core version 3.1 • redis. • Dependency injection

# Instruccions to execution
1. Dowload or clone the code
3. Modify, if you want, over the file appsettings.json the connection redis variables: 
   "Redis": {
    "Host": "xx",
    "Port": "xx",
    "Pass": "xx"
  },
4. Run the project

# To test
you can use the next postman collection https://www.getpostman.com/collections/e5407f3e0f13496a23fe there is all the definition request. (File - Import , select link and paste the link)


# Endpoinds
1. Endpoint de creación de nuevas ruletas: (POST) /WheelOfFortune/create
2. Endpoint de apertura de ruleta: (POST) /WheelOfFortune/open?prmId=<<guidid>>
3. Endpoint de apuesta a un número: (POST) /WheelOfFortune/Bet 
   body:
      {
      "IdWheelOfFortune":<<guidid of the wheel>>,
      "BetNumber":<<number value of the bet or null if the bet is by color>>,
      "Money":"Money of the bet",
      "ColorRed":<<boolen, true when the bet is for red color, false if the bet is for black color and null if the bet is by number>>
      }
  header:
    userID -> string value
4. Endpoint de cierre apuestas dado un id (POST) WheelOfFortune/close?prmId=<<guidid>>
