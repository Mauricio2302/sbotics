//OBR DIAMANTE :)
//By: Maurício Calvet ;-;

//Declaração de Variáveis   

//Sensores internos do robô
    string bussola(){
        double bussola;
        bussola = Bot.Compass;
        string printbussola = $"{bussola}";
        return printbussola;
    }

    double bussolaValues(){
        return Bot.Compass;
    }
    
    string Inclination(){
        double inclination;
        inclination = Bot.Inclination;
        string printinclination = $"{inclination}";
        return printinclination;
    }

    double InclinationValues(){
        return Bot.Inclination;
    }    
//Bussola <90 ta inclinado pra esquerda
//Bussola >90 ta inclinado pra direita
//Inclinação <350 ta inclinado pra cima

// Servo Motores
    Servomotor motor1 = Bot.GetComponent<Servomotor>("motor1");
    Servomotor motor2 = Bot.GetComponent<Servomotor>("motor2");
    Servomotor motor3 = Bot.GetComponent<Servomotor>("motor3");
    Servomotor motor4 = Bot.GetComponent<Servomotor>("motor4");
    Servomotor braço  = Bot.GetComponent<Servomotor>("braço");
    Servomotor mão = Bot.GetComponent<Servomotor>("mão");
    Servomotor garra = Bot.GetComponent<Servomotor>("garra");

//Declaração dos sensores de cor
    ColorSensor sensorM = Bot.GetComponent<ColorSensor>("sensorM");
    ColorSensor sensorL = Bot.GetComponent<ColorSensor>("sensorL");
    ColorSensor sensorL2 = Bot.GetComponent<ColorSensor>("sensorL2");
    ColorSensor sensorR = Bot.GetComponent<ColorSensor>("sensorR");
    ColorSensor sensorR2 = Bot.GetComponent<ColorSensor>("sensorR2");
    ColorSensor sensorB = Bot.GetComponent<ColorSensor>("sensorB");
    ColorSensor sensorB2 = Bot.GetComponent<ColorSensor>("sensorB2");

// Variáveis de cor Digitais
    bool corL;
    bool corL2;
    bool corM;
    bool corR;
    bool corR2;

// Variáveis de cor Analogicas
    string colorM;
    string colorL;
    string colorL2;
    string colorR;
    string colorR2;

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

// Variáveis do sensor Ultrassonico
    double distanceF;
    double distanceD;
    double distanceE;
    double distanceG;
    double distanceT;

// Constantes do PID
    const double Kp = 90;
    double P;
    double error = 0;

// Valores base para Velocidade e Força
    int velocidade = 125;
    int força = 125;

// Começo da execução do Codigo
    async Task Main (){
        await GUP();
        double valor_inicial = bussolaValues();
        while(true){
            await Time.Delay(50);
            IO.Print(bussolaValues().ToString());
            IO.Print(InclinationValues().ToString());
            /*ReadDistance();
            LineFollower();
        //Casos do verde
            if (verde_R()){
                await R90();
            }

            if (verde_L()){
                await L90();
                continue;
            }
            }
        //Casos do obstaculo
     /*       if (distanceF <= 2){
            await obstaculo();
        }*/          
    }
    }
    


//Funções dos motores
//Motores 1 && 3 = Motores da esquerda
//Motores 2 && 4 = Motores da direita   
    void destravar(){
	    motor1.Locked = false;
	    motor2.Locked = false;
	    motor3.Locked = false;
	    motor4.Locked = false;
    }

    void frente (double força, double velocidade) {
        destravar();
        motor1.Apply(força, velocidade);
        motor3.Apply(força, velocidade);
        motor2.Apply(força, velocidade);
        motor4.Apply(força, velocidade);
    }

    void tras (double força, double velocidade){
        destravar();
        motor1.Apply(força, -velocidade);
        motor3.Apply(força, -velocidade);
        motor2.Apply(força, -velocidade);
        motor4.Apply(força, -velocidade);
    }

    void direita (double força, double velocidade){
        destravar();
        motor1.Apply(força, velocidade);
        motor3.Apply(força, velocidade);
        motor2.Apply(força, -velocidade);
        motor4.Apply(força, -velocidade);
    }

    void esquerda (double força, double velocidade){
        destravar();
        motor1.Apply(força, -velocidade);
        motor3.Apply(força, -velocidade);
        motor2.Apply(força, velocidade);
        motor4.Apply(força, velocidade);
    }

    void travar(){
	    motor1.Locked = true;
	    motor2.Locked = true;
	    motor3.Locked = true;
	    motor4.Locked = true;
    }

//Leitura dos sensores ultrassonicos  
    void ReadDistance(){
        distanceF = ultra_F.Analog;
        distanceE = ultra_E.Analog;
        distanceD = ultra_D.Analog;
        distanceT = ultra_T.Analog;
        distanceG = ultra_G.Analog;
    }

//Leitura dos Sensores de cor (Branco && Preto)
    string ReadLine(){
	    corM = !sensorM.Digital;
	    corL = !sensorL.Digital;
	    corL2 = !sensorL2.Digital;
	    corR = !sensorR.Digital;
	    corR2 = !sensorR2.Digital;
//Organização da ordem dos sensores de acordo com sua posição no robô
    string line = $"{corL2} {corL} {corM} {corR} {corR2}";
        line = line.Replace("False", "0");
        line = line.Replace("True", "1");
          return line;
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

    async Task Cases()
//Enquanto a linha estiver na direita o erro sera positivo
//Enquanto a linha estiver na esquerda o erro sera negativo
    {
        switch (ReadLine())
        {
            case "0 0 0 0 0":
                error = 0;
                break;
            case "1 1 1 1 1":
                error = 0;
                break;
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
                error = 4.5;
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
                error = -4.5;
                break;
            case "1 0 0 0 0":
                error = -4;
                break;
            case "1 1 1 1 0":
                error = -4;
                break;
            case "0 1 1 1 1":
                error = 4;
                break; 
//Casos de 90º
            case "1 1 1 0 0": 
                error = -4.5;
                break;
            case "0 0 1 1 1":
                error = 4.5;
                break;
        }
    }

//Seguidor de linha baseado no PID(Erros)
    void LineFollower(){
        Cases();
        P = Kp*error;
        destravar();
        motor1.Apply(500, 160+P);
        motor3.Apply(500, 160+P);
        motor2.Apply(500, 160-P);
        motor4.Apply(500, 160-P);
    }

//Função de girar 90 Graus no verde
    async Task R90(){
        frente(200,200);
        await Time.Delay(700);
        direita(500, 500);
        await Time.Delay(1100);
        frente(200,200);
        await Time.Delay(700);
    }

    async Task L90(){
        frente(200,200);
        await Time.Delay(700);
        esquerda(500, 500);
        await Time.Delay(1100);
        frente(200,200);
        await Time.Delay(700);
    }
//Função de subir a garra
    async Task GUP(){
        braço.Locked = false;
        braço.Apply(500, 300);
        await Time.Delay(3000);
    }

//Função desviar de obstaculo
    async Task obstaculo(){
        direita(500,500);
        await Time.Delay(1100);
        frente(200,200);
    if(distanceE > 20){
        frente(200,200);
        await Time.Delay(500);
        esquerda(500,500);
        await Time.Delay(1100);
        frente(200,200);
    }
    }
    /*if(linhaM){
        break;
    }
    }*/
//Caso onde a linha preta se encontra no meio
    bool linhaM(){
        if(ReadLine()=="0 0 1 0 0"){
        return true;
    }
        else{
        return false;
        }
    }

//Caso onde todos os sensores veem a linha preta
    bool full_line(){
        if(ReadLine()=="1 1 1 1 1"){
        return true;
    }
        else{
        return false;
        }
    }

//Caso onde a linha preta se encontra nos sensores da esquerda e no meio
    bool linhaE90(){
        if(ReadLine()=="1 1 1 0 0"){
        return true;
    }
        else{
        return false;
        }
    }

//Caso onde a linha preta se encontra nos sensores da direita e no meio
    bool linhaD90(){
        if(ReadLine()=="0 0 1 1 1"){
        return true;
    }
        else{
        return false;
        }
    }
//
    bool linhaD(){
        if(ReadLine()=="0 0 0 1 1" || ReadLine()=="0 0 0 0 1"){
        return true;
    }
        else{
        return false;
        }
    }

//
    bool linhaE(){
        if(ReadLine()=="1 1 0 0 0" || ReadLine()=="1 0 0 0 0"){
        return true;
    }
        else{
        return false;
        }
    }

    string posição(){
        if (valor_inicial > 89 && <91) {
            N = 90
            NO = < 90 && > 0
            SO = <360 && > 270
            S = 270
            SE = <270 && > 180
            NE = <180 && > 90
        } 

        if (valor_inicial > 0 && < 2) {
            N = 0
            NO = < 360 && > 270
            SO = <270 && > 180
            S = 180
            SE = <180 && > 90
            NE = <90 && > 0
        } 

        if (valor_inicial > 179 && <181) {
            N = 180
            NO = <180 && > 90
            SO = <90 && > 0
            S = 0
            SE = <360 && > 270
            NE = < 270 && > 180
        }
        if (valor_inicial > 359 && <361) {
            N = 360
            NO = <360 && > 270
            SO = <90 && > 0
            S = 0
            SE = <360 && > 270
            NE = < 270 && > 180
        }
         
    }
