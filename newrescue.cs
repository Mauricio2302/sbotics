string motor1 = "motor1", motor2 = "motor2", motor3 = "motor3", motor4="motor4",garra = "garra", braço = "braço",
mão = "mão", cor_M, cor_L2, cor_R2, cor_R, cor_L, cor_B, cor_B2, bagColorL, bagColorR, distanciafrente, distanciadireita, distanciagarra;
bool toque1, toque2, toque3 = false;

bool resgate = false;
bool percurso = true;
bool withvictmin = false;
bool leftcube = false;

async Task Main(){
await Time.Delay(50);
destravarB();
levantarB(300,300);
await Time.Delay(700);
travarB();
await Time.Delay(300);



while (true){
if (prata("sensorM") ){
frente(300,300);
await Time.Delay(1000);
sensores_cor();
	destravar();
destravarG();
	resgate = true;
	percurso = false;
	await Time.Delay(50);
}

await Time.Delay(50);
double obstaculo = 3;

UltrasonicSensor Ultra_B = Bot.GetComponent<UltrasonicSensor>("ultraBack");
UltrasonicSensor ultra_F = Bot.GetComponent<UltrasonicSensor>("ultra_F");
UltrasonicSensor ultra_D = Bot.GetComponent<UltrasonicSensor>("ultra_D");
UltrasonicSensor ultra_G = Bot.GetComponent<UltrasonicSensor>("ultra_G");
/*UltrasonicSensor ultra_G2 = Bot.GetComponent<UltrasonicSensor>("ultra_G2");
UltrasonicSensor ultra_G3 = Bot.GetComponent<UltrasonicSensor>("ultra_G3");*/

distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
double distancia = ultra_F.Analog;

distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
double distanciaD = ultra_D.Analog;

distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
double distanciaG = ultra_G.Analog;


Color cor_VR = Bot.GetComponent<ColorSensor>("sensorR").Analog;
double redR = cor_VR.Red; 
double greenR = cor_VR.Green; 
double blueR = cor_VR.Blue; 
Color cor_VR2 = Bot.GetComponent<ColorSensor>("sensorR2").Analog;
double redR2 = cor_VR2.Red; 
double greenR2 = cor_VR2.Green; 
double blueR2 = cor_VR2.Blue; 
Color cor_VL = Bot.GetComponent<ColorSensor>("sensorL").Analog;
double redL = cor_VL.Red; 
double greenL = cor_VL.Green; 
double blueL = cor_VL.Blue; 
Color cor_VL2 = Bot.GetComponent<ColorSensor>("sensorL2").Analog;
double redL2 = cor_VL2.Red; 
double greenL2 = cor_VL2.Green; 
double blueL2 = cor_VL2.Blue; 
Color cor_VM = Bot.GetComponent<ColorSensor>("sensorM").Analog;
double redM = cor_VM.Red; 
double greenM = cor_VM.Green; 
double blueM = cor_VM.Blue; 

IO.PrintLine ("distancia:" + distanciafrente);
while(percurso == true){
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
sensores_cor();


await Time.Delay(50);

	if(distancia < 2 && distancia >= 0 && cor_M == "Preto"){
		await desvio();
}

if(distancia == -1 ){
distancia = 300;
}
IO.PrintLine ("Percurso");
IO.PrintLine ("distancia:" + distancia.ToString());
destravar();
sensores_cor();
travarG();



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


if(distanciaG <= 2.1 && distanciaG >=0 && distancia>=10 && (prata("sensorB") || prata("sensorB2") )){
await pegarcubo();
}






if(verde_A()){
frente(100,100);
await Time.Delay(150);
cor_verde();
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
	cor_verde();
if(cor_L == "Preto" || cor_L2 == "Preto" || cor_M == "Preto" || cor_R == "Preto" || cor_R2 == "Preto"){
frente(110, 110);
await Time.Delay(450);
cor_verde();
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
	cor_verde();
if(cor_L == "Preto" || cor_L2 == "Preto" || cor_M == "Preto" || cor_R == "Preto" || cor_R2 == "Preto"){
frente(110, 110);
await Time.Delay(450);
cor_verde();
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
if(distancia < 3 && distancia > 0){
destravar();
	        direita(275,275);
	        await Time.Delay(1892);
frente(-240,-240);
await Time.Delay(2050);
frente(200,200);
}

if(verde_A() && leftcube == false){
	await dropar();
    leftcube = true;
    IO.Print("deixei, papai");
}

while(withvictmin == false && leftcube == true){
    await Time.Delay(10);
IO.Print ("Searching the baseball bat");
sensores_cor();
destravarG();
await Time.Delay(350);
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
if(distancia < 3 && distancia > 0){
destravar();
	        direita(275,275);
	        await Time.Delay(1892);
frente(-240,-240);
await Time.Delay(1550);
frente(200,200);
}
if(distanciaD >2 && distanciaD < 18){
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
IO.Print("Achei o baseball bat");
sensores_cor();
frente(100,100);
await Time.Delay(110);
	        direita(275,275);
	        await Time.Delay(1892);
frente(-200, -200);
await Time.Delay(3200);
frente(200,200);
while(distanciaG > 2 ){
    await Time.Delay(10);
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
sensores_cor();
frente(200,200);
}
if(distanciaG < 2 && distanciaG > 0  && withvictmin == false){
   await pegarvitima();
    if(isgreen("bagColorR") || isgreen("bagColorL")){
		    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);
        withvictmin = false;
        IO.Print("No baseball bat");
        await Time.Delay(2000);
    }
    if(bagColorR == "Preto" || bagColorR == "Branco" || bagColorL == "Preto" || bagColorL == "Branco"){
        withvictmin = true;
	 await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);
        IO.Print("Peguei o baseball bat");
        while(Ultra_B.Analog > 2){
        frente(-200, -200);
        await Time.Delay(10);
    }
	    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);
	        esquerda(275,275);
	        await Time.Delay(1892);
    }
}

}

}

while(withvictmin){
	    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);
    
    if(bagColorL == "Preto" || bagColorR == "Preto"){
		    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);
        while(distancia > 2 ){
			    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	
            frente(200, 200);
            await Time.Delay(1);
        }
        if(distancia <= 2){
	        direita(275,275);
	        await Time.Delay(1892);
        }
        if(red("sensorM")){
		await droparvitima();
        }
        
    } else{
        while(distancia > 2 ){
			    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();

            frente(200, 200);
            await Time.Delay(1);
        }
		   if(distancia <= 2){
		    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);
	        direita(275,275);
	        await Time.Delay(1892);
	}
        }
        if(verde_A()){
			    await Time.Delay(10);
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
	distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
	distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
	distancia = ultra_F.Analog;
	distanciaG = ultra_G.Analog;
	distanciaD = ultra_D.Analog;
	sensores_cor();
	frente(200,200);	
			await droparvitima();
        }
    }

    
}
}
}





void frente(double forca, double velocidade){
	Bot.GetComponent<Servomotor>(motor1).Apply(forca, velocidade);
	Bot.GetComponent<Servomotor>(motor2).Apply(forca, velocidade);
	Bot.GetComponent<Servomotor>(motor3).Apply(forca, velocidade);
	Bot.GetComponent<Servomotor>(motor4).Apply(forca, velocidade);
}

void destravar(){
	Bot.GetComponent<Servomotor>(motor1).Locked = false;
	Bot.GetComponent<Servomotor>(motor2).Locked = false;
	Bot.GetComponent<Servomotor>(motor3).Locked = false;
	Bot.GetComponent<Servomotor>(motor4).Locked = false;

}

void direita(double forca, double velocidade){
	Bot.GetComponent<Servomotor>(motor1).Apply(forca, velocidade);
	Bot.GetComponent<Servomotor>(motor2).Apply(forca, -velocidade);
	Bot.GetComponent<Servomotor>(motor3).Apply(forca, velocidade);
	Bot.GetComponent<Servomotor>(motor4).Apply(forca,-velocidade);
}

void esquerda(double forca, double velocidade){
	Bot.GetComponent<Servomotor>(motor1).Apply(forca, -velocidade);
	Bot.GetComponent<Servomotor>(motor2).Apply(forca, velocidade);
	Bot.GetComponent<Servomotor>(motor3).Apply(forca, -velocidade);
	Bot.GetComponent<Servomotor>(motor4).Apply(forca, velocidade);
}



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

void travar(){
	Bot.GetComponent<Servomotor>(motor1).Locked = true;
	Bot.GetComponent<Servomotor>(motor2).Locked = true;
	Bot.GetComponent<Servomotor>(motor3).Locked = true;
	Bot.GetComponent<Servomotor>(motor4).Locked = true;
}

void cor_verde(){
Color cor_VR = Bot.GetComponent<ColorSensor>("sensorR").Analog;
double redR = cor_VR.Red; 
double greenR = cor_VR.Green; 
double blueR = cor_VR.Blue; 
Color cor_VR2 = Bot.GetComponent<ColorSensor>("sensorR2").Analog;
double redR2 = cor_VR2.Red; 
double greenR2 = cor_VR2.Green; 
double blueR2 = cor_VR2.Blue; 
Color cor_VL = Bot.GetComponent<ColorSensor>("sensorL").Analog;
double redL = cor_VL.Red; 
double greenL = cor_VL.Green; 
double blueL = cor_VL.Blue; 
Color cor_VL2 = Bot.GetComponent<ColorSensor>("sensorL2").Analog;
double redL2 = cor_VL2.Red; 
double greenL2 = cor_VL2.Green; 
double blueL2 = cor_VL2.Blue; 
Color cor_VM = Bot.GetComponent<ColorSensor>("sensorM").Analog;
double redM = cor_VM.Red; 
double greenM = cor_VM.Green; 
double blueM = cor_VM.Blue; 
}

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

bool red(string sensor){
Color cor = Bot.GetComponent<ColorSensor>(sensor).Analog;
double redm = cor.Red; 
double green = cor.Green; 
double blue = cor.Blue; 
if(redm > blue && redm > green){
return true;


}
else {
return false;
}
}


bool verde_R(){
if(isgreen("sensorR") || isgreen("sensorR2")){
 return true;
}

else {
return false;
}
}

bool verde_L(){
if(isgreen("sensorL") || isgreen("sensorL2")){
 return true;
}

else {
return false;
}
}

bool verde_A(){
if((isgreen("sensorR") || isgreen("sensorR2")) || (isgreen("sensorM")) || (isgreen("sensorL") || isgreen("sensorL2"))){
 return true;
}

else {
return false;
}
}

void levantarG(double forca, double velocidade){
Bot.GetComponent<Servomotor>(garra).Apply(forca, velocidade);

}

void destravarG(){
	Bot.GetComponent<Servomotor>(garra).Locked = false;
}

void travarG(){
	Bot.GetComponent<Servomotor>(garra).Locked = true;

}
void levantarB(double forca, double velocidade){
Bot.GetComponent<Servomotor>(braço).Apply(forca, velocidade);

}
void destravarB(){
	Bot.GetComponent<Servomotor>(braço).Locked = false;
}

void travarB(){
	Bot.GetComponent<Servomotor>(braço).Locked = true;

}

void destravarM(){
	Bot.GetComponent<Servomotor>(mão).Locked = false;
}

void travarM(){
	Bot.GetComponent<Servomotor>(mão).Locked = true;

}
void levantarM(double forca, double velocidade){
Bot.GetComponent<Servomotor>(mão).Apply(forca, velocidade);

}

void touch(){
toque1 = Bot.GetComponent<TouchSensor>("toque1").Digital;
toque2 = Bot.GetComponent<TouchSensor>("toque2").Digital;
toque3 = Bot.GetComponent<TouchSensor>("toque3").Digital;

}

bool alinhar(){
   if(toque1 == false || toque2 == false || toque3 ==  false){
    return true;
    }
    else {
        return false;
    }
}

async Task pegarcubo(){
	direita(100,100);
	await Time.Delay(100);
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
    await Time.Delay(2060);
	destravarM();
    travar();
    levantarM(-250, -250);
    await Time.Delay(2060);
	travarM();
    levantarB(170,170);
    await Time.Delay(1900);
	destravarM();
    levantarM(250, 250);
    await Time.Delay(1160);
    levantarM(-250, -250);
    await Time.Delay(1160);
    travarM();
    destravar();
}

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

async Task desvio(){
	destravar();
	frente(200, 200);
	await Time.Delay(1000);
	frente(-100,-100);
	await Time.Delay(700);
	direita(300,300);
	await Time.Delay(1832);
	frente(100, 100);
	await Time.Delay(3100);
	esquerda(300,300);
	await Time.Delay(1832);
	frente(100, 100);
	await Time.Delay(7100);
	esquerda(300,300);
	await Time.Delay(1832);
	frente(100, 100);
	await Time.Delay(4050);
	direita(300,300);
	await Time.Delay(1832);
	frente(-200,-200);
	await Time.Delay(1050);
}

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
//
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
            withvictmin = false;
}
