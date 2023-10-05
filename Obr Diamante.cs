//OBR DIAMANTE :)
//By: Maurício Calvet ;-;

//Declaração de Variáveis Globais

//Definição dos motores de movimento
    string motor1 = "motor1", motor2 = "motor2", motor3 = "motor3", motor4="motor4";

//Definição dos motores da garra
    string garra = "garra", braço = "braço", mão = "mão";

//Definição das variaveis que armazenam os dados lidos pelos sensores de cor
    string cor_M, cor_L2, cor_R2, cor_R, cor_L, cor_B, cor_B2, bagColorL, bagColorR;

//Definição das variaveis que armazenam os dados lidos pelos sensores de distancia
    string distanciafrente, distanciadireita, distanciagarra, distanciaesquerda;

//Definição das variaveis que armazenam os dados lidos pelos sensores de toque
    bool toque1, toque2, toque3 = false;

//Fases do percurso
    bool resgate = false;
    bool percurso = true;
    bool final = false;
    bool withvictmin = false;
    bool leftcube = false;
    bool searching = false;

//Começo da execução do codigo
    async Task Main(){
        await Time.Delay(50);
        destravarB();
        levantarB(300,300);
        await Time.Delay(700);
        travarB();
        await Time.Delay(300);
        while (true){
            await Time.Delay(50);
            double obstaculo = 3;

//Definição e atualização dos valores do sensor de distancia
            UltrasonicSensor ultra_F = Bot.GetComponent<UltrasonicSensor>("ultra_F");
            UltrasonicSensor ultra_D = Bot.GetComponent<UltrasonicSensor>("ultra_D");
            UltrasonicSensor ultra_G = Bot.GetComponent<UltrasonicSensor>("ultra_G");
            distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
            double distancia = ultra_F.Analog;
            distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
            double distanciaD = ultra_D.Analog;
            distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
            double distanciaG = ultra_G.Analog;

//começo da execução do seguidor de linha
            while(percurso == true){
                distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
                distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
                distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
                distancia = ultra_F.Analog;
                distanciaG = ultra_G.Analog;
                sensores_cor();
                await Time.Delay(50);

//função para desviar de obstaculo
                if(distancia < 2 && distancia >= 0 && cor_M == "Preto"){
                    await desvio();
                }
//função de mudar o valor infinito que originalmente seria -1 para 300
                if(distancia == -1 ){
                    distancia = 300;
                }

                destravar();
                sensores_cor();
                travarG();

//Caso da leitura da fita prata na entrada da area de resgate
                if (prata("sensorM") ){
                    frente(150, 150);
                    await Time.Delay(1200);
                    sensores_cor();
                    destravar();
                    destravarG();
                    resgate = true;
                    percurso = false;
                    await Time.Delay(50);
                }

//Caso da leitura da fita vermelho no final da pista
                if(red("sensorM")){
                    while(true){
                        await Time.Delay(50);
                        travar();
                    }
                }

//Caso de pegar o cubo de resgate
                if(distanciaG <= 2.1 && distanciaG >=0 && distancia>=10 && (prata("sensorB") || prata("sensorB2") )){
                    await pegarcubo();
                }

//Casos do verde

//Caso onde o verde se encontra em qualquer um dos nossos 5 sensores
                if(verde_A()){
                    frente(100,100);
                    await Time.Delay(150);
    
//Caso onde o verde se encontra nos sensores da esquerda e da direita para efetuar o 180º
                    if (verde_R() && verde_L()){
                        destravar();
                        frente(110,110);
                        await Time.Delay(150);
                        direita(300,300);
                        await Time.Delay(3624);
                        frente(110,110);
                        await Time.Delay(300);
                        continue;
                    }

//Caso onde o verde se encontra nos sensores da esquerda 
                    if (verde_L()){
                        destravar();
                        frente(110,110);
                        await Time.Delay(750);
                        esquerda(300,300);
                        await Time.Delay(1622);
                        frente(110,110);
                        await Time.Delay(300);
                        continue;
                    }

//Caso onde o verde se encontra nos sensores da direita
                    if (verde_R()){
                        destravar();
                        frente(110,110);
                        await Time.Delay(750);
                        direita(300,300);
                        await Time.Delay(1622);
                        frente(110,110);
                        await Time.Delay(300);
                        continue;
                    }	

                }

//Funções do seguidor de linha basico (preto e branco)
                if (cor_L == "Preto" && cor_M == "Branco" && cor_R == "Branco"){
                    destravar();
                    esquerda(140,140);
                    await Time.Delay(200);
                    continue;
                } 

                if (cor_L2 == "Preto" && cor_M == "Branco" && cor_R == "Branco"){
                    destravar();
                    esquerda(140,140);
                    await Time.Delay(200);
                    continue;
                }

                if (cor_L == "Branco" && cor_M == "Branco" && cor_R == "Preto"){
                    destravar();
                    direita(140,140);
                    await Time.Delay(60);
                    continue;
                }

                if (cor_L == "Branco" && cor_M == "Branco" && cor_R2 == "Preto"){
                    destravar();
                    direita(140,140);
                    await Time.Delay(60);
                    continue;
                }

                if (cor_L == "Branco" && cor_M == "Preto" && cor_R == "Preto" && cor_R2 == "Preto"){
                    destravar();
                    frente(110,110);
                    await Time.Delay(450);
                    sensores_cor();
        
                    if(cor_L == "Preto" || cor_L2 == "Preto" || cor_M == "Preto" || cor_R == "Preto" || cor_R2 == "Preto"){
                        frente(110, 110);
                        await Time.Delay(450);
                        
                        if(verde_R() && verde_L()){
                            frente(100, 100);
                            await Time.Delay(1000);
                        }
                    }
                    else {
                        frente(-100,-100);
                        await Time.Delay(100);
                        direita(300,300);
                        await Time.Delay(1622);
                        frente(-200,-200);
                        await Time.Delay(200);
                        continue;
                    }
                    continue;
                }

                if (cor_L == "Preto" && cor_L2 == "Preto" && cor_M == "Preto" && cor_R == "Branco"){
                    destravar();
                    frente(110,110);
                    await Time.Delay(450);
                    sensores_cor();
        
                    if(cor_L == "Preto" || cor_L2 == "Preto" || cor_M == "Preto" || cor_R == "Preto" || cor_R2 == "Preto"){
                        frente(110, 110);
                        await Time.Delay(450);
                        if(verde_R() && verde_L()){
                            frente(100, 100);
                            await Time.Delay(1000);
                        }
                    }
                    else {
                        frente(-100,-100);
                        await Time.Delay(100);
                        esquerda(300,300);
                        await Time.Delay(1622);
                        frente(-200,-200);
                        await Time.Delay(200);
                        continue;
                    }
                    continue;
                }

                if (cor_L == "Preto" && cor_M == "Preto" && cor_R == "Preto"){
                    frente(150, 150);
                    await Time.Delay(600);
                }	

                else {
                frente(155,155);
                }

            }   

//Começo da execução do "while resgate"
            while(resgate == true){
                await Time.Delay(50);
                IO.Print ("Resgate");
                sensores_cor();
                destravarG();
                frente(200,200);
                await Time.Delay(350);
                distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
                distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
                distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
                distancia = ultra_F.Analog;
                distanciaG = ultra_G.Analog;
                distanciaD = ultra_D.Analog;

//Caso para girar o robo de acordo com a parede do drc
                if(distancia < 3 && distancia > 0){
                    destravar();
                    direita(275,275);
                    await Time.Delay(1892);
                    frente(-240,-240);
                    await Time.Delay(2050);
                    frente(200,200);
                }

//Caso onde ele encontra a cor verde e tbm oq ela ainda n tinha perdido o bv
                if(verde_A() && leftcube == false){
                    await dropar();
                    leftcube = true;
                    IO.Print("deixei, papai");
                    searching = true;
                }

//Caso onde ele está procurando a saida da area de resgate
	            while(searching == true){
                    await Time.Delay(50);
                    IO.Print ("procurando");
                    sensores_cor();
                    destravarG();
                    frente(200,200);
                    await Time.Delay(350);
                    distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
                    distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
                    distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
                    distancia = ultra_F.Analog;
                    distanciaG = ultra_G.Analog;
                    distanciaD = ultra_D.Analog;

                    if(distancia < 3 && distancia > 0 && distanciaD >= 40){
                        destravar();
                        frente(200,200);
                        await Time.Delay(1000);
                        esquerda(275,275);
                        await Time.Delay(1892);
                    }	

                    if(prata("sensorM")){
                        frente(-240,-240);
                        await Time.Delay(1050);
                        direita(275,275);
                        await Time.Delay(1892);
                    }

                    if(linhaM()){
                        resgate = false;
                        percurso = true;
                        searching = false;
                    }

                    if(distancia < 3 && distancia > 0){
                        destravar();
                        direita(275,275);
                        await Time.Delay(1892);
                        frente(-240,-240);
                        await Time.Delay(2050);
                        frente(200,200);
                    }	
                }

	        }
        }
    }





//Funções dos motores
//Motores 1 && 3 = Motores da esquerda
//Motores 2 && 4 = Motores da direita   

//Função para mover os 4 motores para frente
    void frente(double forca, double velocidade){
        Bot.GetComponent<Servomotor>(motor1).Apply(forca, velocidade);
        Bot.GetComponent<Servomotor>(motor2).Apply(forca, velocidade);
        Bot.GetComponent<Servomotor>(motor3).Apply(forca, velocidade);
        Bot.GetComponent<Servomotor>(motor4).Apply(forca, velocidade);
    }

//Função para destravar os motores para liberar a locomoção
    void destravar(){
        Bot.GetComponent<Servomotor>(motor1).Locked = false;
        Bot.GetComponent<Servomotor>(motor2).Locked = false;
        Bot.GetComponent<Servomotor>(motor3).Locked = false;
        Bot.GetComponent<Servomotor>(motor4).Locked = false;
    }


//Função para mover os 4 motores de forma que faça ele se mover para a direita
    void direita(double forca, double velocidade){
        Bot.GetComponent<Servomotor>(motor1).Apply(forca, velocidade);
        Bot.GetComponent<Servomotor>(motor2).Apply(forca, -velocidade);
        Bot.GetComponent<Servomotor>(motor3).Apply(forca, velocidade);
        Bot.GetComponent<Servomotor>(motor4).Apply(forca,-velocidade);
    }

//Função para mover os 4 motores de forma que faça ele se mover para a esquerda
    void esquerda(double forca, double velocidade){
        Bot.GetComponent<Servomotor>(motor1).Apply(forca, -velocidade);
        Bot.GetComponent<Servomotor>(motor2).Apply(forca, velocidade);
        Bot.GetComponent<Servomotor>(motor3).Apply(forca, -velocidade);
        Bot.GetComponent<Servomotor>(motor4).Apply(forca, velocidade);
    }

//Função para travar os motores e bloquear a locomoção
    void travar(){
        Bot.GetComponent<Servomotor>(motor1).Locked = true;
        Bot.GetComponent<Servomotor>(motor2).Locked = true;
        Bot.GetComponent<Servomotor>(motor3).Locked = true;
        Bot.GetComponent<Servomotor>(motor4).Locked = true;
    }

//Leitura dos Sensores de cor
    void sensores_cor(){
        cor_M = Bot.GetComponent<ColorSensor>("sensorM").Analog.ToString();
        cor_L2 = Bot.GetComponent<ColorSensor>("sensorL2").Analog.ToString();
        cor_R2 = Bot.GetComponent<ColorSensor>("sensorR2").Analog.ToString();
        cor_R = Bot.GetComponent<ColorSensor>("sensorR").Analog.ToString();
        cor_L = Bot.GetComponent<ColorSensor>("sensorL").Analog.ToString();
        cor_B = Bot.GetComponent<ColorSensor>("sensorB").Analog.ToString();
        cor_B2 = Bot.GetComponent<ColorSensor>("sensorB2").Analog.ToString();
        bagColorL = Bot.GetComponent<ColorSensor>("bagColorL").Analog.ToString();
        bagColorR = Bot.GetComponent<ColorSensor>("bagColorR").Analog.ToString();
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
        double redm = cor.Red; 
        double green = cor.Green; 
        double blue = cor.Blue; 
        if(redm * 0.9 > blue && redm * 0.9 > green){
            return true;
        }
        else {
        return false;
        }
    }

//Localização do Verde

//Verde na direita
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

//Verde em qualquer um dos 5 sensores
    bool verde_A(){
        if((isgreen("sensorR") || isgreen("sensorR2")) || (isgreen("sensorM")) || (isgreen("sensorL") || isgreen("sensorL2"))){
            return true;
        }
        else {
            return false;
        }
    }

//Função de levantar a garra
    void levantarG(double forca, double velocidade){
        Bot.GetComponent<Servomotor>(garra).Apply(forca, velocidade);
    }

//Função de levantar o braço
    void levantarB(double forca, double velocidade){
        Bot.GetComponent<Servomotor>(braço).Apply(forca, velocidade);
    }

//Função de levantar a mão
    void levantarM(double forca, double velocidade){
        Bot.GetComponent<Servomotor>(mão).Apply(forca, velocidade);
    }


//Função de destravar o braço
    void destravarB(){
        Bot.GetComponent<Servomotor>(braço).Locked = false;
    }

//Função de travar o motor braço
    void travarB(){
        Bot.GetComponent<Servomotor>(braço).Locked = true;
    }

//Função de destravar a mão
    void destravarM(){
        Bot.GetComponent<Servomotor>(mão).Locked = false;
    }

//Função de travar o motor mão
    void travarM(){
        Bot.GetComponent<Servomotor>(mão).Locked = true;
    }

//Função de destravar a garra
    void destravarG(){
        Bot.GetComponent<Servomotor>(garra).Locked = false;
    }

//Função de travar o motor garra
    void travarG(){
        Bot.GetComponent<Servomotor>(garra).Locked = true;
    }

//Função para ver oque o sensorde toque esta vendo
    void touch(){
        toque1 = Bot.GetComponent<TouchSensor>("toque1").Digital;
        toque2 = Bot.GetComponent<TouchSensor>("toque2").Digital;
        toque3 = Bot.GetComponent<TouchSensor>("toque3").Digital;
    }

// Caso onde o alinhamento é feito para melhor desempenho do robo
    bool alinhar(){
        if(toque1 == false || toque2 == false || toque3 ==  false){
            return true;
        }
        else {
            return false;
        }
    }

//Caso onde o robo vai pegar o cubo de resgate
    async Task pegarcubo(){
        direita(100,100);
        await Time.Delay(300);
        frente(-200, -200);
        await Time.Delay(1100);
        travar();
        destravarM();
        levantarM(300, 300); 
        await Time.Delay(1660); 
        travarM();
        destravarB();
        levantarB(-200,-200); 
        await Time.Delay(800);
        destravar();
        frente(160, 160);
        await Time.Delay(1660);
        destravarM();
        travar();
        levantarM(-330, -330);
        await Time.Delay(1860);
        travarM();
        levantarB(140,140);
        await Time.Delay(1700);
        destravarM();
        levantarM(250, 250);
        await Time.Delay(1160);
        levantarM(-250, -250);
        await Time.Delay(1160);
        travarM();
        destravar();
    }

//Função de dropar o cubo no verde
    async Task dropar(){
        destravar();
        destravarB();
        levantarB(-300,-300);
        await Time.Delay(300);
        frente(130,130);
        await Time.Delay(1450);
        direita(275,275);
        await Time.Delay(1892);
        frente(-300,-300);
        await Time.Delay(2050);
        frente(160,160);
        await Time.Delay(3850);
        travar();
        destravarG();
        await Time.Delay(100);
        levantarG(350,350);
        await Time.Delay(1100);
        levantarG(-350,-350);
        await Time.Delay(400);
        travarG();
        destravar();
        frente(110,110);
        await Time.Delay(300);
        destravarB();
        levantarB(300,300);
        await Time.Delay(300);
        withvictmin = false;
    }

//função de desviar de obstaculo
    async Task desvio(){
        frente(200,-200);
        await Time.Delay(300);
        direita(300,300);
        await Time.Delay(1700);
        sensores_cor();
        while(linhaM() == false){
            sensores_cor();
            await Time.Delay(50);
            destravar();
            Bot.GetComponent<Servomotor>(motor2).Apply(500, 520);
            Bot.GetComponent<Servomotor>(motor1).Apply(400, 15);
            Bot.GetComponent<Servomotor>(motor4).Apply(500, 520);
            Bot.GetComponent<Servomotor>(motor3).Apply(400, 15);
        }
        direita(300,300);
        await Time.Delay(1500);
        frente(200,-200);
        await Time.Delay(1000);
    }

//Caso onde a linha preta esta no meio siginifica que é preto msm
    bool linhaM(){
        if(cor_M == "Preto"){
            return true;
        }
        else{
            return false;
        }
    }

//Função de pegar as vitimas
    async Task pegarvitima(){
    frente(-200, -200);
        await Time.Delay(1150);
        travar();
        destravarM();
        levantarM(400, 400); 
        await Time.Delay(1660); 
        destravarB();
        levantarB(-220,-220); 
        await Time.Delay(800);
        destravar();
        direita(200, 200);
        await Time.Delay(135);
        frente(160, 160);
        await Time.Delay(1860);
        travar();
        levantarM(-250, -250);
        await Time.Delay(1860);
        travarM();
        levantarB(220,220);
        await Time.Delay(2100);
        destravarM();
        levantarM(250, 250);
        await Time.Delay(960);
        levantarM(-250, -250);
        await Time.Delay(960);
        travarM();
        destravar();
    }
    
//Função de dropar as vitimas
    async Task droparvitima(){
        destravar();
        destravarB();
        levantarB(-300,-300);
        await Time.Delay(300);
        frente(130,130);
        await Time.Delay(1450);
        direita(275,275);
        await Time.Delay(1822);
        frente(-300,-300);
        await Time.Delay(2050);
        frente(160,160);
        await Time.Delay(3850);
        travar();
        destravarG();
        await Time.Delay(100);
        levantarG(270,270);
        await Time.Delay(1100);
        levantarG(-270,-270);
        await Time.Delay(400);
        travarG();
        destravar();
        frente(110,110);
        await Time.Delay(300);
        destravarB();
        levantarB(300,300);
        await Time.Delay(300);
    }
