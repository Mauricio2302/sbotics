//OBR estadualv1?

// Motors:
Servomotor motRF = Bot.GetComponent<Servomotor>("rightMotor");
Servomotor motLF = Bot.GetComponent<Servomotor>("leftMotor");
Servomotor motRB = Bot.GetComponent<Servomotor>("backRightMotor");
Servomotor motLB = Bot.GetComponent<Servomotor>("backLeftMotor");

Servomotor arm  = Bot.GetComponent<Servomotor>("arm");
Servomotor hand = Bot.GetComponent<Servomotor>("hand");

Servomotor bag = Bot.GetComponent<Servomotor>("bag");

// Follow line (5) sensor:
ColorSensor colorLL = Bot.GetComponent<ColorSensor>("colorLL"); //ok
ColorSensor colorL = Bot.GetComponent<ColorSensor>("colorLeft"); //ok
ColorSensor colorM = Bot.GetComponent<ColorSensor>("colorMiddle"); //ok     
ColorSensor colorR = Bot.GetComponent<ColorSensor>("colorRight"); //ok
ColorSensor colorRR = Bot.GetComponent<ColorSensor>("colorRR"); //ok
ColorSensor colorRF = Bot.GetComponent<ColorSensor>("frontRight");
ColorSensor colorLF = Bot.GetComponent<ColorSensor>("frontLeft");

//ColorSensor bagR = Bot.GetComponent<ColorSensor>("bagColorR"); //ok
//ColorSensor bagL = Bot.GetComponent<ColorSensor>("bagColorL"); //ok

UltrasonicSensor ultraDown = Bot.GetComponent<UltrasonicSensor>("ultraDown")     ; //ok
UltrasonicSensor ultraMidRight = Bot.GetComponent<UltrasonicSensor>("ultraMidRight") ; //ok
UltrasonicSensor ultraMid = Bot.GetComponent<UltrasonicSensor>("ultraMid")      ; //ok
UltrasonicSensor ultraRight = Bot.GetComponent<UltrasonicSensor>("ultraRight")    ; //ok
UltrasonicSensor ultraLeft = Bot.GetComponent<UltrasonicSensor>("ultraLeft")     ; //ok
UltrasonicSensor ultraBack = Bot.GetComponent<UltrasonicSensor>("ultraBack")     ; //ok   

TouchSensor touchL = Bot.GetComponent<TouchSensor>("touchLeft"); //ok
TouchSensor touchM = Bot.GetComponent<TouchSensor>("touchMid"); //ok 
TouchSensor touchR = Bot.GetComponent<TouchSensor>("touchRight"); //ok 

// Color Read by the sensors
bool corLL ;
bool corL  ;
bool corM  ;
bool corR  ;
bool corRR ;

const double Kp = 90; //90
double P;
double error = 0;

byte rescueSpeed = 250;

const int rootDelay = 50;

byte basespeed = 125;
byte power = 125;

bool leftBool;
bool rightBool;

bool has_LeftGreen;
bool has_RightGreen;

bool precursor;


async Task Main (){//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //print("Hello, I'm obsidian");
    //bool goFollowLine = true;


    clearConsole();
    // print("Hello, I'm obsidian");
    IO.OpenConsole();
    //upArm();
    await upArm();

    // print(getLine());

    bool debug = false;
    bool start = !debug;
    while(debug){
       // await bagMove();
       followLine();
        await Time.Delay(rootDelay);
    }

    while(start){
        
        bool possible_right_crossing = (getLine()=="0 0 1 1 1"|| getLine()=="0 1 1 1 1");
        bool possible_left_crossing = (getLine()=="1 1 1 0 0" || getLine()=="1 1 1 1 0");
        bool absolute_crossing = (getLine()=="1 1 1 1 1");
        bool isObstacle = (uDown() < 2 && uMid() <= 2);
        bool isCube = (uDown() <= 0.8 && uMid() >= 6);
        bool isRescue = (isBlue(colorM)); 


        if(isRed(colorM)){
            while(true){
                print("I am done.");
                stop();
                await Time.Delay(rootDelay);      
            }
        }




        //print(getLine());

        followLine();
        if(hasSomeGreen()){
            has_LeftGreen = hasLeftGreen();
            has_RightGreen = hasRightGreen();
            while(hasSomeGreen()){
                front(500, basespeed);
                await Time.Delay(rootDelay);
            }
            Lock();
            precursor = true;
            if(has_RightGreen && !has_LeftGreen){
                while(precursor){
                    if(isWhite(colorR) || isWhite(colorRR)){
                        break;
                    }
                    if(has_RightGreen){
                    front(500, basespeed);
                    await Time.Delay(800);
                    right(500, basespeed);
                    await Time.Delay(5000);
                    precursor = false;
                    }
                }
            }
            precursor = true;
            if(has_LeftGreen && !has_RightGreen){
                while(precursor){
                    if(isWhite(colorL) || isWhite(colorLL)){
                        break;
                    }
                    if(has_LeftGreen){
                    front(500, basespeed);
                    await Time.Delay(800);
                    left(500,basespeed);
                    await Time.Delay(5000);
                    precursor = false;
                    }
                }
            }
            precursor = true;
            if(has_LeftGreen && has_RightGreen){
                while(precursor){
                    if((isWhite(colorL) || isWhite(colorLL))  ||(isWhite(colorR) || isWhite(colorRR))){
                        break;
                    }
                    if(has_LeftGreen && has_RightGreen){
                        front(500, basespeed);
                        await Time.Delay(400);
                        right(500,basespeed);
                        await Time.Delay(10000);
                        precursor = false;
                    }
                }
            }




            
            /* while(true){
                stop();
                await Time.Delay(50);
            } */
        }
           if(possible_left_crossing || possible_right_crossing){
               // stop();
            }   

            if(isCube){
                print($"{"is rescue kit here!"}"); 
                while (true){
                    await getItem();
                    await Time.Delay(rootDelay);
                    break;
                }
            }
            double distance = ultraMid.Analog;
    if (distance == -1){
        distance = 999;
    }
    if(distance < 3){
        back(power, basespeed);
        await Time.Delay(700);
        rightBool = true;
        if(rightBool){
            if(getCompass()>=268){
                while(getCompass() < 358){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((getCompass() - atual) < 90){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        rightBool = false;
        }
        
        front(power, basespeed);
        await Time.Delay(3000);
        leftBool = true;
        if(leftBool){
            if(getCompass()<=91){
                while(getCompass() > 2){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((atual - getCompass()) < 90){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        leftBool = false;
        }

        front(power, basespeed);
        await Time.Delay(7500);
        leftBool = true;
        if(leftBool){
            if(getCompass()<=91){
                while(getCompass() > 2){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((atual - getCompass()) < 90){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        leftBool = false;
        }
        front(power, basespeed);
        await Time.Delay(2500);
        rightBool = true;
        if(rightBool){
            if(getCompass()>=268){
                while(getCompass() < 358){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((getCompass() - atual) < 90){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        rightBool = false;
        }
        back(power, basespeed);
        await Time.Delay(1300);
    };

        await Time.Delay(rootDelay);

       if(isRescue){
            await rescue();
            await Time.Delay(rootDelay);
        }

        await Time.Delay(rootDelay);

       if(isRescue){
        //print(uLeft().ToString());
            await rescue();
            await Time.Delay(rootDelay);
        }
    }

       
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

void stop()
{
   front(0 , 0);
   Lock();
}

string getLine(){
	corLL = !colorLL.Digital ;
    corL  = !colorL.Digital  ;
    corM  = !colorM.Digital  ;
    corR  = !colorR.Digital  ;
    corRR = !colorRR.Digital ;

string line = $"{corLL} {corL} {corM} {corR} {corRR}";
 line = line.Replace("False", "0");
 line = line.Replace("True", "1");

  return line;
}

async Task readFullLine(){
   switch(getLine()) {
        case "0 0 0 0 0":
         error = 0;
            break;
        case "1 1 1 1 1": //
         error = 0;       //
            break;       //
        case "0 0 1 0 0":
            error = 0;
            break;
        case "0 0 1 1 0":
            error = 1;
            break;
        case "0 0 0 1 0":
            error = 2;
            break;
        case "0 0 0 1 1":
            error = 3;
            break;
        case "0 0 0 0 1":
            error = 4;
            break;
        
        case "0 1 1 0 0":
            error = -1;
            break;
        
        case "0 1 0 0 0":
            error = -2;
            break;
        
        case "1 1 0 0 0":
            error = -3;
            break;
        
        case "1 0 0 0 0":
            error = -4;
            break;
      case "1 1 1 0 0": // daqui
            error = -4.5;
            break;
        case "0 0 1 1 1":
            error = 4.5;
            break;
        case "1 1 1 1 0":
            error = -4;
            break;
        case "0 1 1 1 1":
            error = 4;
            break;       // até aqui
            
   }

}

void followLine(){
    readFullLine();
    P = Kp*error;

    unLock();
    motLF.Apply(500, 150+P);
    motLB.Apply(500, 150+P);
    motRF.Apply(500, 150-P);
    motRB.Apply(500, 150-P);
}

bool isWhite(ColorSensor sensor){
    string color = sensor.Analog.ToString();
    return (color == "Branco");
}
bool isGreen(ColorSensor sensor, double margin=0.9){
    Color reading = sensor.Analog;
    double real = (reading.Green * margin);
    return ((real>reading.Red) && (real>reading.Blue));
}

bool isBlue(ColorSensor sensor, double margin=0.95){
    Color reading = sensor.Analog;
    double real = (reading.Blue * margin);
    return ((real>reading.Red) && (real>reading.Green));
}

bool isRed(ColorSensor sensor, double margin=0.9){
    Color reading = sensor.Analog;
    double real = (reading.Red * margin);
    return ((real>reading.Blue) && (real>reading.Green));
}

void front (double force, double speed) {
    unLock();
    motLF.Apply(force, speed);
    motLB.Apply(force, speed);
    motRF.Apply(force, speed);
    motRB.Apply(force, speed);
}

void back (double force, double speed){
    unLock();
    motLF.Apply(force, -speed);
    motLB.Apply(force, -speed);
    motRF.Apply(force, -speed);
    motRB.Apply(force, -speed);
}

void right (double force, double speed){
    unLock();
    motLF.Apply(force, speed);
    motLB.Apply(force, speed);
    motRF.Apply(force, -speed);
    motRB.Apply(force, -speed);
}

void left (double force, double speed){
    unLock();
    motLF.Apply(force, -speed);
    motLB.Apply(force, -speed);
    motRF.Apply(force, speed);
    motRB.Apply(force, speed);
}

void print(string text)
{
    //IO.OpenConsole();
    IO.Print(text);
}

void clearConsole(){
    IO.ClearPrint();
}

    void unLock ()
{
    motLF.Locked = false;
    motRF.Locked = false;
    motLB.Locked = false;
    motRB.Locked = false;
}

    void Lock (){
    motLF.Locked = true;
    motRF.Locked = true;
    motLB.Locked = true;
    motRB.Locked = true;
    }

    bool hasSomeGreen() {
        return isGreen(colorL) || isGreen(colorLL) || isGreen(colorM) || isGreen(colorR) || isGreen(colorRR);
    }

    bool hasAllGreen() {
        return isGreen(colorL) && isGreen(colorLL) && isGreen(colorM) && isGreen(colorR) && isGreen(colorRR);
    }

    bool hasLeftGreen() {
        return (isGreen(colorL) || isGreen(colorLL)) || ((isGreen(colorR) && isGreen(colorRR) && isGreen(colorM)));
    }

    bool hasRightGreen() {
        return (isGreen(colorR) || isGreen(colorRR)) || ((isGreen(colorL) && isGreen(colorLL) && isGreen(colorM)));
    }

double uDown(){
    double distance = ultraDown.Analog;
    
        if (distance == -1){
            distance = 999;
        }
    return distance;
}

double uBack(){
    double distance = ultraBack.Analog;

    if(distance == -1){
        distance = 999;
    }
    return distance;
}

double uMid(){
    double distance = ultraMid.Analog;

    if (distance == -1){
        distance = 999;
        }
    return distance;
}

double uRight(){
    double distance = ultraRight.Analog;

    if (distance == -1){
        distance = 999;
    }
    return distance;
}

double uLeft(){
    double distance = ultraLeft.Analog;

        if (distance == -1){
            distance = 999;
        }
    return distance;
}

double uMidRight(){
    double distance = ultraMidRight.Analog;

        if (distance == -1){
         distance = 999;

        }
    return distance;
}

async Task getItem (){
     
    stop();
    await Time.Delay(400);

    //trás
    back(500, 200);
    await Time.Delay(500); //500
    stop();
    await Time.Delay(500);

    //abaixa o ombro
    arm.Locked = false;
    arm.Apply(500, -100);
    await Time.Delay(2200);
    arm.Locked = true;

    //alinha?
    right(500, 300);
    await Time.Delay(200);
    stop();
    await Time.Delay(500);

    //frente
    front(500, 200);
    await Time.Delay(300);
    stop();
    await Time.Delay(500);

    //fecha a mão
    hand.Locked = false;
    hand.Apply(500,-200);
    await Time.Delay(1500);
    hand.Locked = true;

    //sobe
    arm.Locked = false;
    arm.Apply(500, 100);
    await Time.Delay(1800);
    arm.Locked = true;

    hand.Locked = false;
    hand.Apply(500,100);
    await Time.Delay(1000);
    hand.Locked = true;

    left(500, 300);
    await Time.Delay(200);
    stop();
    await Time.Delay(500);
    
}

async Task upArm ()
{
    arm.Locked = false;
    arm.Apply(500, 200);
    await Time.Delay(1000);
    arm.Locked = true;

    hand.Locked = false;
    hand.Apply(500,200);
    await Time.Delay(500);
    hand.Locked = true;
}

async Task rescue(){
    //print("is Rescue!!!!");

    stop();
    await Time.Delay(200);            
    front(500, rescueSpeed);
    await Time.Delay(2500);

    stop();
    await Time.Delay(200);            
    right(300, rescueSpeed);
    await Time.Delay(100);

    await alignB();
    stop();
    await Time.Delay(200);

    bool hasUp = (uLeft() < 20);
    bool hasRight = (uDown() < 19);

    //print($"{uDown().ToString()} || {uRight().ToString()}");

    if (hasUp || hasRight){
        //IO.Print("Are you crazy?");
        if (hasUp && !hasRight){     
            print($"has up  {uLeft().ToString()} || {uDown().ToString()}");
            left(power, basespeed);
            await Time.Delay(5750);

            while(uDown() > 3.7){
                front(power, basespeed);
                await Time.Delay(rootDelay);
            }

            if(isRed(colorLF)){
                print("is red!");
                while(uDown() < 14){
                    right(power, basespeed);
                    await Time.Delay(rootDelay);
                }


                
                
                while(uDown() > 4){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                

                while(uDown() < 10){
                    right(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                

                while(uDown() > 4){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                

                if(isGreen(colorLF)){
                print("is green!");
                while(uDown() < 13.5){
                    right(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                
                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                right(power, basespeed);
                await Time.Delay(6000);
                back(power, basespeed);
                await Time.Delay(500);
                //await bagMove();
                }
            }
            if(isGreen(colorLF)){
                print("is green!");
                while(uDown() < 13.5){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                left(power, basespeed);
                await Time.Delay(6000);

                
                back(power, basespeed);
                await Time.Delay(500);
               // await bagMove();
                
                }

            
        }

        if(hasRight && !hasUp){
            print($"has right  {uLeft().ToString()} || {uDown().ToString()}");
            while(uDown() > 4){
                front(power, basespeed);
                await Time.Delay(rootDelay);
            }
            
            if(isRed(colorLF)){
                print("is red!");
                while(uDown() < 14){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 4){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() < 10){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 4){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                if(isGreen(colorLF)){
                print("is green!");
                while(uDown() < 13.5){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                left(power, basespeed);
                await Time.Delay(6000);

                
                back(power, basespeed);
                await Time.Delay(500);
               // await bagMove();
                
                }
                
                }
            if(isGreen(colorLF)){
                print("is green!");
                while(uDown() < 13.5){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                left(power, basespeed);
                await Time.Delay(6000);

                
                back(power, basespeed);
                await Time.Delay(500);
                //await bagMove();
                
                }
                      
                        
             
                
                
            
            }
                
            
        }

        if(hasUp && hasRight){
            print($"has both  {uLeft().ToString()} || {uDown().ToString()}");






        while(uDown() > 4){
                front(power, basespeed);
                await Time.Delay(rootDelay);
            }
            
            if(isRed(colorLF)){
                print("is red!");
                while(uDown() < 14){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                left(power, basespeed);
                await Time.Delay(6000);

                front(power, basespeed);
                await Time.Delay(15000);

                left(power, basespeed);
                await Time.Delay(12000);

                back(power, basespeed);
                await Time.Delay(2000);
                //await bagMove();

                


                /* while(uDown() < 10){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 4){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                } */

                if(isGreen(colorLF)){
                print("is green!");
                while(uDown() < 13.5){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                left(power, basespeed);
                await Time.Delay(6000);

                
                back(power, basespeed);
                await Time.Delay(500);
                //await bagMove();
                
                }
                
                }
            if(isGreen(colorLF)){
                print("is green!");
                while(uDown() < 13.5){
                    left(power, basespeed);
                    await Time.Delay(rootDelay);
                }
                while(uDown() > 8){
                    front(power, basespeed);
                    await Time.Delay(rootDelay);
                }

                left(power, basespeed);
                await Time.Delay(6000);

                
                back(power, basespeed);
                await Time.Delay(500);
                //await bagMove();
                
                }
        }
    } 



async Task right90 (){
    right(500, 300);
    await Time.Delay(1500);
}

async Task left90 (){
    left(500, 300);
    await Time.Delay(1500);
}

string getCompassString(){
    double compass;
    compass = Bot.Compass;
    string printCompass = $"{compass}";
    return printCompass;
}

double getCompass(){
    return Bot.Compass;
}

async Task alignB (){
    //print(getCompass().ToString());

        bool turnRight = false;
        bool turnLeft  = false;

            if(getCompass() > 180){
                turnRight = true;
            } else{
                turnLeft = true;
            }

            if(turnLeft){
            if(getCompass()>=268){
                while(getCompass() < 358){
                    right(power, basespeed);
                   // IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((getCompass() - atual) < 90){
                    right(power, basespeed);
                   // IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        turnLeft = false;
        }

        if(turnRight){
            if(getCompass()<=91){
                while(getCompass() > 2){
                    left(power, basespeed);
                   // IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((atual - getCompass()) < 90){
                    left(power, basespeed);
                    //IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        turnRight    = false;
        }

}

async Task getObstacle(){

    bool rightBool;
    bool leftBool;

    double distance = ultraMid.Analog;

    if (distance == -1){
        distance = 999;
    }

    if(distance < 3){
        back(power, basespeed);
        await Time.Delay(700);
        rightBool = true;
        if(rightBool){
            if(getCompass()>=268){
                while(getCompass() < 358){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((getCompass() - atual) < 90){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        rightBool = false;
        }
        
        front(power, basespeed);
        await Time.Delay(3500);
        leftBool = true;
        if(leftBool){
            if(getCompass()<=91){
                while(getCompass() > 2){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((atual - getCompass()) < 90){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        leftBool = false;
        }

        front(power, basespeed);
        await Time.Delay(8000);
        leftBool = true;
        if(leftBool){
            if(getCompass()<=91){
                while(getCompass() > 2){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((atual - getCompass()) < 90){
                    left(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        leftBool = false;
        }
        front(power, basespeed);
        await Time.Delay(3800);
        rightBool = true;
        if(rightBool){
            if(getCompass()>=268){
                while(getCompass() < 358){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
            else{
                double atual = getCompass();
                while((getCompass() - atual) < 90){
                    right(power, basespeed);
                    IO.Print(getCompassString());
                    await Time.Delay(rootDelay);
                }
            }
        rightBool = false;
        }
        back(power, basespeed);
        await Time.Delay(1300);
    }
}

/*async Task bagMove (){

    arm.Locked = false;
    arm.Apply(500, -100);
    await Time.Delay(2200);
    arm.Locked = true;
    arm.Apply(0, 0);
    
    do{
        print("tentando mover bag");
        bag.Locked = false;
        bag.Apply(500,400);
        await Time.Delay(rootDelay);
    } while(!isRed(bagR) || !isRed(bagL));

    await Time.Delay(1000);
    do{
        print("voltando!!");
        bag.Locked = false;
        bag.Apply(500,-300);
        await Time.Delay(rootDelay);
    } while(isRed(bagR) || isRed(bagL));

    bag.Locked = false;
    bag.Apply(500, -400);
    await Time.Delay(5000);
    bag.Locked = true;

}*/

async Task alignTouch(){

    while (!touchL.Digital && !touchM.Digital && ! touchR.Digital);{
        back(500, 200);
        await Time.Delay(rootDelay);

    } 

}
