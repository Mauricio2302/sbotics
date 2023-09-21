//OBR DIAMANTE :)
//By: Maurício Calvet ;-;

//Declaração de Variáveis

// Servo Motores
    Servomotor motor1 = Bot.GetComponent<Servomotor>("motor1");
    Servomotor motor2 = Bot.GetComponent<Servomotor>("motor2");
    Servomotor motor3 = Bot.GetComponent<Servomotor>("motor3");
    Servomotor motor4 = Bot.GetComponent<Servomotor>("motor4");
    Servomotor braço  = Bot.GetComponent<Servomotor>("braço");
    Servomotor mão = Bot.GetComponent<Servomotor>("mão");
    Servomotor garra = Bot.GetComponent<Servomotor>("garra");

// Sensores de cor
    ColorSensor sensorM = Bot.GetComponent<ColorSensor>("sensorM");
    ColorSensor sensorL = Bot.GetComponent<ColorSensor>("sensorL");
    ColorSensor sensorL2 = Bot.GetComponent<ColorSensor>("sensorL2");
    ColorSensor sensorR = Bot.GetComponent<ColorSensor>("sensorR");
    ColorSensor sensorR2 = Bot.GetComponent<ColorSensor>("sensorR2)");
    ColorSensor sensorB = Bot.GetComponent<ColorSensor>("sensorB");
    ColorSensor sensorB2 = Bot.GetComponent<ColorSensor>("sensorB2");
    bool cor_B;
    bool cor_B2;
    bool corL;
    bool corL2;
    bool corM;
    bool corR;
    bool corR2;
    bool bagColorL;
    bool bagColorR;

// Sensores de toque
    TouchSensor toque_E = Bot.GetComponent<TouchSensor>("toque_E"); 
    TouchSensor toque_D = Bot.GetComponent<TouchSensor>("toque_D");  
    TouchSensor toque_M = Bot.GetComponent<TouchSensor>("toque_M"); 

// Fases do percurso
    bool resgate;
    bool percurso;
    bool final;
    bool withvictmin;
    bool leftcube;

// Sensores de distancia
    UltrasonicSensor ultra_D = Bot.GetComponent<UltrasonicSensor>("ultra_D"); 
    UltrasonicSensor ultra_F = Bot.GetComponent<UltrasonicSensor>("ultra_F");
    UltrasonicSensor ultra_E = Bot.GetComponent<UltrasonicSensor>("ultra_E"); 
    UltrasonicSensor ultra_G = Bot.GetComponent<UltrasonicSensor>("ultra_G");
    UltrasonicSensor ultra_T = Bot.GetComponent<UltrasonicSensor>("ultra_T");   

// Constantes do PID
    const double Kp = 90;
    double P;
    double error = 0;
    byte rescueSpeed = 250;
    const int rootDelay = 50;
    int velocidade = 125;
    int força = 125;

// Começo da execução do Codigo
async Task Main (){
    while(true){
        frente(100,100);
        IO.PrintLine("cor_L");
        IO.PrintLine("cor_L2");
        IO.PrintLine("cor_M");
        IO.PrintLine("cor_R");
        IO.PrintLine("cor_R2");

}        
}

//Funções dos motores
    void destravar(){
	    motor1.Locked = false;
	    motor2.Locked = false;
	    motor3.Locked = false;
	    motor4.Locked = false;
    }

    void frente (double força, double velocidade) {
        destravar();
        motor1.Apply(força, velocidade);
        motor2.Apply(força, velocidade);
        motor3.Apply(força, velocidade);
        motor4.Apply(força, velocidade);
    }

    void tras (double força, double velocidade){
        destravar();
        motor1.Apply(força, -velocidade);
        motor2.Apply(força, -velocidade);
        motor3.Apply(força, -velocidade);
        motor4.Apply(força, -velocidade);
    }

    void direita (double força, double velocidade){
        destravar();
        motor1.Apply(força, velocidade);
        motor2.Apply(força, velocidade);
        motor3.Apply(força, -velocidade);
        motor4.Apply(força, -velocidade);
    }

    void esquerda (double força, double velocidade){
        destravar();
        motor1.Apply(força, -velocidade);
        motor2.Apply(força, -velocidade);
        motor3.Apply(força, velocidade);
        motor4.Apply(força, velocidade);
    }

    void travar(){
	    motor1.Locked = true;
	    motor2.Locked = true;
	    motor3.Locked = true;
	    motor4.Locked = true;
    }

//Leitura dos Sensores de cor (Branco && Preto)
    string ReadLine(){
	    corM = !sensorM.Digital;
	    corL = !sensorL.Digital;
	    corL2 = !sensorL2.Digital;
	    corR = !sensorR.Digital;
	    corR2 = !sensorR2.Digital;
    }

//Leitura dos Sensores de cor (Verde && Prata && Vermleho)

//Verde
    bool isgreen(string sensor){
        Color cor = Bot.GetComponent<ColorSensor>(sensor).Analog;
        double red = cor.Red; 
        double green = cor.Green; 
        double blue = cor.Blue; 
            if(green * 0.85 > red && green * 0.85 > blue){
            return true;
        }      
            else {
            return false;
        }
    }
//Localização do Verde

// Verde na direita
    bool verde_R(){
        if(isgreen("sensorR") || isgreen("sensorR2")){
         return true;
     }

        else {
        return false;
     }
    }

//Verde na esquerda
    bool verde_L(){
        if(isgreen("sensorL") || isgreen("sensorL2")){
        return true;
     }

        else {
        return false;
     }   
    }

//Verde em qualquer um dos Sensores
    bool verde_A(){
        if((isgreen("sensorR") || isgreen("sensorR2")) || (isgreen("sensorM")) || (isgreen("sensorL") || isgreen("sensorL2"))){
        return true;
     }

        else {
        return false;
     }
    }

//Prata
    bool prata(string sensor){
        Color cor = Bot.GetComponent<ColorSensor>(sensor).Analog;
        double red = cor.Red; 
        double green = cor.Green; 
        double blue = cor.Blue; 
            if(blue * 0.98 > red && blue * 0.98 > green){
            return true;
        }
            else {
        return false;
        }
    }

//Vermelho
    bool red(string sensor){
        Color cor = Bot.GetComponent<ColorSensor>(sensor).Analog;
        double red = cor.Red; 
        double green = cor.Green; 
        double blue = cor.Blue; 
            if(red > blue && red > green){
            return true;
        }
            else {
        return false;
        }
    }


