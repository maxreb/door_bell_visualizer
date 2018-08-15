#include <Arduino.h>


#define LED 13
#define DOOR_BELL 23

void setup() {
    pinMode(LED,OUTPUT);
    pinMode(DOOR_BELL,INPUT);
    Serial.begin(9600);

    // put your setup code here, to run once:
}

void loop() {
   // int read = 
    //Serial.println(read);
    digitalWrite(LED,digitalRead(DOOR_BELL));
    //delay(10);
    // put your main code here, to run repeatedly:
}