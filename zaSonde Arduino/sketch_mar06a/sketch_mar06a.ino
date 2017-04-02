#define ADR1  0x0A
#define ADR2  0x14
#define ADR3  0x1E
#define ADR4  0x28

void setup() {

  pinMode(LED_BUILTIN, OUTPUT);
  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }


}
int i = 0;
void loop() { // run over and over
  byte randTemperatureH = random(10);
  byte randTemperatureM = random(10);
  byte randTemperatureL = random(10);
  byte randHumidityH = random(10);
  byte randHumidityL = random(10);
  
  if (Serial.available()) {
   
    if(i == 1){

      digitalWrite(LED_BUILTIN, LOW);
      i = 0;
    }
    else{
      i = 1;
    digitalWrite(LED_BUILTIN, HIGH);
    }

    delay(50);
    char read_buffer = Serial.read();
    byte send_buffer[9];
    

    if(read_buffer == '1'){      
      send_buffer[0] = 0x21;
      send_buffer[1] = ADR1;
      send_buffer[2] = 0x30;
      send_buffer[3] = randTemperatureH;
      send_buffer[4] = randTemperatureM;
      send_buffer[5] = randTemperatureL;
      send_buffer[6] = randHumidityH;
      send_buffer[7] = randHumidityL;
      send_buffer[8] = 0x1B;
      Serial.write(send_buffer, 9);          
    }
    else if(read_buffer == '2'){
      send_buffer[0] = 0x21;
      send_buffer[1] = ADR2;
      send_buffer[2] = 0x30;
      send_buffer[3] = randTemperatureH;
      send_buffer[4] = randTemperatureM;
      send_buffer[5] = randTemperatureL;
      send_buffer[6] = randHumidityH;
      send_buffer[7] = randHumidityL;
      send_buffer[8] = 0x1B;
      Serial.write(send_buffer, 9);
    }
    else if(read_buffer == '3'){
      send_buffer[0] = 0x21;
      send_buffer[1] = ADR3;
      send_buffer[3] = randTemperatureH;
      send_buffer[4] = randTemperatureM;
      send_buffer[5] = randTemperatureL;
      send_buffer[6] = randHumidityH;
      send_buffer[7] = randHumidityL;
      send_buffer[8] = 0x1B;
      Serial.write(send_buffer, 9);
    }
    else if(read_buffer == '4'){
      send_buffer[0] = 0x21;
      send_buffer[1] = ADR4;
      send_buffer[2] = 0x30;
      send_buffer[3] = randTemperatureH;
      send_buffer[4] = randTemperatureM;
      send_buffer[5] = randTemperatureL;
      send_buffer[6] = randHumidityH;
      send_buffer[7] = randHumidityL;
      send_buffer[8] = 0x1B;
      Serial.write(send_buffer, 9);
    }
    

  }
  

}
