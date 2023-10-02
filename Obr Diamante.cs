//OBR DIAMANTE :)
//By: Maurício Calvet ;-;

//Declaração de Variáveis Globais

//Sensores internos do robô (Bussola e Inclinação)
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
        double valor_direita = 0;
        double valor_esquerda = 0;
        double valor_inicial = 0;
        bool tortoD = false;
        bool tortoE = false;

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
    ColorSensor bagColorR = Bot.GetComponent<ColorSensor>("bagColorR");
    ColorSensor bagColorL = Bot.GetComponent<ColorSensor>("bagColorL");

// Variáveis de cor Digitais
    bool corL;
    bool corL2;
    bool corM;
    bool corR;
    bool corR2;
    bool bagL;
    bool bagR;
    bool corB;
    bool corB2;

// Variáveis de cor Analogicas
    string colorM;
    string colorL;
    string colorL2;
    string colorR;
    string colorR2;
    string BGL;
    string BGR;
    string colorB;
    string colorB2;

// Sensores de toque
    TouchSensor toque_E = Bot.GetComponent<TouchSensor>("toque_E"); 
    TouchSensor toque_D = Bot.GetComponent<TouchSensor>("toque_D");  
    TouchSensor toque_M = Bot.GetComponent<TouchSensor>("toque_M"); 

// Fases do percurso
    bool resgate = false;
    bool percurso = true;
    bool final;
    bool withvictmin;
    bool leftcube;
    bool resgatando = false;

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
    const double Kp = 95;
    double P;
    double error = 0;

// Valores base para Velocidade e Força
    int velocidade = 125;
    int força = 125;

// Começo da execução do Codigo
    async Task Main (){
        await GUP();
        posição();
        valor_inicial = bussolaValues();
        await Time.Delay(2000);
        while(true){
            while(percurso == true){
                await Time.Delay(50);
                ReadDistance();
                LineFollower();

                //Caso onde o sensor identifica a fita prata da area de resgate
                if(prata("sensorM")){
                    percurso = false;
                    resgate = true;
                }

                //Casos para pegar o cubo
                if(distanceG >= 1.5 && distanceG <= 2 && distanceF >= 300 && prata("sensorB")|| prata("sensorB2")){
                    await GetCube();
                }

                //Casos do obstaculo
                if(distanceF <= 4 && distanceF >= 0){
                    await obstaculo();
                }

                //Casos do verde
                if(verde_A()){
                    if (verde_R() && verde_L()){
                        await LR();
                        continue;
                    }

                    if (verde_L()){
                        await L90();
                        continue;
                    }

                    if (verde_R()){
                        await R90();
                        continue;
                    }
                }

                //Falso cruzamento do verde
                if(full_line()){
                    frente(200,200);
                    await Time.Delay(1000);
                }

                //90º para a direita
                if(linhaD90()){
                    direita(500, 500);
                    await Time.Delay(800);
                    frente(200, -200);
                    await Time.Delay(300);
                }

                //90º para a esquerda
                if(linhaE90()){
                    esquerda(500, 500);
                    await Time.Delay(800);
                    frente(200, -200);
                    await Time.Delay(300);
                }
                    
                    //Casos para adicionar velocidade para subir a rampa
                    /*if(InclinationValues() <= 355 && InclinationValues() >= 300){
                        while(InclinationValues() <= 355 && InclinationValues()>= 300){
                            await Time.Delay(50);
                            LineFollowerR();
                        }
                    }*/

                    //Casos para subtrair velocidade para descer a rampa
                if(InclinationValues() <= 100 && InclinationValues() >= 3){
                    while(InclinationValues() <= 100 && InclinationValues() >= 3){
                        await Time.Delay(50);
                        frente(180,180);
                    }
                }        
            }
            while(resgate == true) {
                await Time.Delay(50);
                valor_inicial = bussolaValues();
                frente(200,200);
                await Time.Delay(200);
                if(prata("bagColorR") || prata("bagColorL")){
                    await give_cube();
                }
            }
        }
    }
    
    
    


//Funções dos motores
//Motores 1 && 3 = Motores da esquerda
//Motores 2 && 4 = Motores da direita   

//Função para destravar os motores para liberar a locomoção
    void destravar(){
	    motor1.Locked = false;
	    motor2.Locked = false;
	    motor3.Locked = false;
	    motor4.Locked = false;
    }

//Função para mover os 4 motores para frente
    void frente (double força, double velocidade) {
        destravar();
        motor1.Apply(força, velocidade);
        motor3.Apply(força, velocidade);
        motor2.Apply(força, velocidade);
        motor4.Apply(força, velocidade);
    }

//Função para mover os 4 motores para trás
    void tras (double força, double velocidade){
        destravar();
        motor1.Apply(força, -velocidade);
        motor3.Apply(força, -velocidade);
        motor2.Apply(força, -velocidade);
        motor4.Apply(força, -velocidade);
    }

//Função para mover os 4 motores de forma que faça ele se mover para a direita
    void direita (double força, double velocidade){
        destravar();
        motor1.Apply(força, velocidade);
        motor3.Apply(força, velocidade);
        motor2.Apply(força, -velocidade);
        motor4.Apply(força, -velocidade);
    }

//Função para mover os 4 motores de forma que faça ele se mover para a esquerda
    void esquerda (double força, double velocidade){
        destravar();
        motor1.Apply(força, -velocidade);
        motor3.Apply(força, -velocidade);
        motor2.Apply(força, velocidade);
        motor4.Apply(força, velocidade);
    }

//Função para travar os motores e bloquear a locomoção
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
        if(distanceF == -1){
            distanceF = 999;
        }
        if(distanceE == -1){
            distanceE = 999;
        }
        if(distanceD == -1){
            distanceD = 999;
        }
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

    async Task Cases(){
//Enquanto a linha estiver na direita o erro sera positivo
//Enquanto a linha estiver na esquerda o erro sera negativo
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
                error = 5;
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
                error = -5;
                break;
            case "1 0 0 0 0":
                error = -4;
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

    void LineFollowerR(){
        Cases();
        P = Kp*error;
        destravar();
        motor1.Apply(400, 400+P);
        motor3.Apply(400, 400+P);
        motor2.Apply(400, 400-P);
        motor4.Apply(400, 400-P);
    }    

//Funções de girar 90 Graus no verde

//90º graus para a direita
    async Task R90(){
        frente(200,200);
        await Time.Delay(1000);
        direita(500, 500);
        await Time.Delay(700);
        frente(200,200);
        await Time.Delay(100);
    }

//90º graus para a esquerda
    async Task L90(){
        frente(200,200);
        await Time.Delay(1000);
        esquerda(500, 500);
        await Time.Delay(700);
        frente(200,200);
        await Time.Delay(100);
    }

//Função de 180º verde
    async Task LR(){
        esquerda(300, 300);
        await Time.Delay(3700); 
    }

//Função de subir a garra
    async Task GUP(){
        braço.Locked = false;
        braço.Apply(500, 300);
        await Time.Delay(2000);
        braço.Locked = true;  
    }

//Função de abaixar a garra
    async Task GDOWN(){
        braço.Locked = false;
        braço.Apply(300, -200);
        await Time.Delay(2000);
        braço.Locked = true;  
    }

//Função de dropar as vitimas e o cubo
    async Task DROP(){
        garra.Locked = false;
        garra.Apply(500,500);
        await Time.Delay(2000);
        garra.Locked = true;
    }

//Função de depositar o cubo
    async Task give_cube(){
        bool cabei = false;
        valor_inicial = bussolaValues();
        ReadDistance();
        while(cabei == false){
            await Time.Delay(50);
            ReadDistance();
            destravar();
            frente(200,200);

            if(verde_A()){
                await GDOWN();
                direita(300, 300);
                await Time.Delay(1000);
                travar();
                await DROP();
                cabei = true;
            }

            if(distanceF <= 2 && distanceF >= 1){
                direita(500, 500);
                await Time.Delay(700);    
            }    
        }
    }

//Função de pegar o cubo
    async Task GetCube(){
        bool peguei = false;
        while(peguei == false){
            travar();
            braço.Locked = false;
            mão.Locked = false;
            mão.Apply(500, 500);
            await Time.Delay(1000);
            braço.Apply(300, -200);
            await Time.Delay(2000);
            mão.Apply(300,-300);
            await Time.Delay(1400);
            braço.Apply(200,150);
            await Time.Delay(2000);
            peguei = true;
            break;
        }   
    }

//Função para o desvio de obstaculo
    async Task obstaculo(){
        ReadDistance();
        bool virei = false;
        bool virei2 = false;
        bool virei3 = false;
        bool cabei = false;
        while(cabei == false){
            await Time.Delay(50);
            while(bussolaValues() <= valor_direita && virei == false){
                ReadDistance();
                await Time.Delay(50);
                direita(200,200);
            }
            virei = true;
            ReadDistance();
        if(distanceE <= 10 && virei == true && virei2 == false ){
            IO.Print("socorro");
            frente(200,200);
            await Time.Delay(1800);
            IO.PrintLine(valor_inicial.ToString());
            IO.PrintLine(bussolaValues().ToString());
                while(bussolaValues() >= valor_inicial){
                    await Time.Delay(50);
                    esquerda(200,200);
                }
        }
            frente(200,200);
            await Time.Delay(1800);
            virei2 = true;
        if(full_line() && virei == true){
            frente(200,200);
            await Time.Delay(500);
            direita(300, 300);
            await Time.Delay(1600);
            cabei = true;
            break;
        }
        
        if(distanceE <= 10 && virei == true && virei2 == true && virei3 == false){
            while(bussolaValues() >= valor_esquerda){
                    await Time.Delay(50);
                    esquerda(200,200);
            }
        }
        virei3 = true;

        if(full_line() && virei2 == true){
            frente(200,200);
            await Time.Delay(500);
            direita(300, 300);
            await Time.Delay(1600);
            cabei = true;
            break;
        }

        }
        }
    
    
//Caso onde a linha preta se encontra no meio
    bool linhaM(){
        if(ReadLine()=="0 0 1 0 0"){
        return true;
        }
        else{
        return false;
        }
    }

//Caso onde não tem linha preta    
    bool nolinha(){
        if(ReadLine()=="0 0 0 0 0"){
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
        if(ReadLine()=="1 1 1 0 0" || ReadLine()=="1 1 1 1 0"){
        return true;
        }
        else{
        return false;
        }
    }

//Caso onde a linha preta se encontra nos sensores da direita e no meio
    bool linhaD90(){
        if(ReadLine()=="0 0 1 1 1" || ReadLine()== "1 1 1 1 0"){
        return true;
        }
        else{
        return false;
        }
    }

//Caso onde a linha preta se encontra nos sensores da direita
    bool linhaD(){
        if(ReadLine()=="0 0 0 1 1" || ReadLine()=="0 0 0 0 1"){
        return true;
        }
        else{
        return false;
        }
    }

//Caso onde a linha preta se encontra nos sensores da esquerda
    bool linhaE(){
        if(ReadLine()=="1 1 0 0 0" || ReadLine()=="1 0 0 0 0"){
        return true;
        }
        else{
        return false;
        }
    }

//Função para calcular as posições do robô baseado na bussola
    void posição(){

        if (valor_inicial >= 315 || valor_inicial <= 45){
             valor_inicial = 1;
             valor_direita = 91;
             valor_esquerda = 271;
        } 

        if (valor_inicial >= 45 && valor_inicial <= 135){
             valor_inicial = 91;
             valor_direita = 181;
             valor_esquerda = 1;
        } 

        if (valor_inicial >= 135 && valor_inicial <=225){
             valor_inicial = 181;
             valor_direita = 271;
             valor_esquerda = 91;
        }

        if (valor_inicial >= 225 && valor_inicial <= 315){
             valor_inicial = 271;
             valor_direita = 350;
             valor_esquerda = 181;
        }
    }

//Função para descobrir se o robô está torto
    void discover(){

        if(valor_inicial == 0){
            if(bussolaValues() >= 1 && bussolaValues() <= 89){
                tortoD = true;
                tortoE = false;
            }

            if(bussolaValues() <= 359 && bussolaValues() >= 271){
                tortoE = true;
                tortoD = false;
            }
        }

        if(valor_inicial == 90){
            if(bussolaValues() >= 91 && bussolaValues() <= 179){
                tortoD = true;
                tortoE = false;
            }

            if(bussolaValues() <= 89 && bussolaValues() >= 1){
                tortoE = false;
                tortoD = true;
            }
        }

        if(valor_inicial == 180){
            if(bussolaValues() >= 181 && bussolaValues() <=269){
                tortoD = true;
                tortoE = false;
            }

            if(bussolaValues() <= 179 && bussolaValues() >= 91){
                tortoE = true;
                tortoD = false;   
            }
        }

        if(valor_inicial == 270){
            if(bussolaValues() >= 271 && bussolaValues() <= 359){
                tortoD = true;
                tortoE = false;
            }

            if(bussolaValues() <= 269 && bussolaValues() >= 181){
                tortoE = true;
                tortoD = false;
            }
        }
    }

    /*
            bool virei = false;
        ReadDistance();
    while(bussolaValues() <= valor_direita){
        await Time.Delay(50);
        direita(200,200);
    if(bussolaValues() == valor_direita){
        frente(200,200);
        await Time.Delay(500);
        continue;    
    }
    }
    while(nolinha()){
        await Time.Delay(50);
        frente(100,100);
    if(distanceE >= 15){
    while(bussolaValues() >= valor_inicial){
        await Time.Delay(50);
        esquerda(200,200);
    if(bussolaValues() == valor_inicial){
        virei = true;
    }
    }     
    }        
    }
    ////////////////////////////////////////////

        

async Task gyrate(double angle, double speed){
  IO.PrintLine(getCompassString());
  double value = getCompass();
  double focus = value + angle;
  
  double focusMarginP = focus + 5;
  double focusMarginM = focus - 5;
   string focusS = $"{focusMarginP}";
  print(focusS); 
  while((getCompass() <= focusMarginM || getCompass() >= focusMarginP)){
    IO.PrintLine(getCompassString());
    right(500, speed);
     if(angle>0){
      right(500, speed);
    }
    else{
      left(500, speed);
    } 
   await Time.Delay(50);
  }
  print("saiu");
  Lock();
  stop();
}
*/

