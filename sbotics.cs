string motor1 = "motor1", motor2 = "motor2", motor3 = "motor3", motor4="motor4",garra = "garra", braço = "braço",
mão = "mão", cor_M, cor_L2, cor_R2, cor_R, cor_L, cor_B, cor_B2, distanciafrente, distanciadireita, distanciagarra;
bool toque1, toque2, toque3 = false;

bool resgate = false;
bool percurso = true;

async Task Main(){
await Time.Delay(50);
destravarB();
levantarB(300,300);
await Time.Delay(500);
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

//distanciagarra2 = Bot.GetComponent<UltrasonicSensor>("ultra_G2").Analog.ToString();
//double distanciaG2 = ultra_G2.Analog;

//distanciagarra3 = Bot.GetComponent<UltrasonicSensor>("ultra_G3").Analog.ToString();
//double distanciaG3 = ultra_G3.Analog;




/*levantar(-300,-300);
await Time.Delay(500);
levantar(300,300);
await Time.Delay(200);*/

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
//IO.PrintLine ("MEIO:" + cor_M);
//IO.PrintLine ("DIREITA:" + cor_R2);
//IO.PrintLine ("ESQUERDA:" + cor_L2);
//IO.PrintLine ("DIREITA2:" + cor_R);
//IO.PrintLine ("ESQUERDA2:" + cor_L);
IO.PrintLine ("distancia:" + distanciafrente);
while(percurso == true){
await Time.Delay(50);
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


if(distanciaG <= 4 && distanciaG >= 3 && distancia == 300 && prata("sensorB")|| prata("sensorB2")){
direita(120,120);
await Time.Delay(60);
frente(140,140);
await Time.Delay(200);
destravarB();
levantarB(-250,-250);
await Time.Delay(300);
destravarM();
levantarM(400, 400);
await Time.Delay(2960);
levantarM(-250, -250);
await Time.Delay(760);
travar();
await Time.Delay(260);
levantarB(150,150);
await Time.Delay(1500);
levantarM(400, 400);
await Time.Delay(760);
levantarM(-250, -250);
await Time.Delay(760);
travarM();

}




distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
//IO.PrintLine ("distanciaG:" + distanciagarra);
	if(distancia < obstaculo && distancia > 1 && cor_M == "Preto"){
	travar();
	await Time.Delay(200);
	destravar();
	frente(-100,-100);
	await Time.Delay(200);
	esquerda(300,300);
	await Time.Delay(1672);
	frente(100, 100);
	await Time.Delay(3500);
	direita(300,300);
	await Time.Delay(1672);
	frente(100, 100);
	await Time.Delay(7500);
	direita(300,300);
	await Time.Delay(1672);
	frente(100, 100);
	await Time.Delay(4050);
	esquerda(300,300);
	await Time.Delay(1672);
	frente(-100,-100);
	await Time.Delay(350);

}

/*if (distanciaG < 4 && distanciaG > 3.5 && distancia == -1){
travar();
destravarB();
levantarB(-500,-500);
await Time.Delay(800);
travarB();
await Time.Delay(200);
destravarM();
levantarM(500, 500);
await Time.Delay(260);



}*/

/*if (distanciaG < 3 && distanciaG > 0){
frente(100,100);
await Time.Delay(1300);
levantarM(-500, -500);
await Time.Delay(600);
travarM();
await Time.Delay(100);
destravarB();
levantarB(300,300);
await Time.Delay(200);
destravarM();
levantarM(500, 500);
await Time.Delay(260);
travarM();




}*/
//sensores_ultra();
//double verde = Analog.Green;

//IO.PrintLine ("ultra-F:" + distanciafrente.ToString());







if(verde_A()){
frente(100,100);
await Time.Delay(150);
cor_verde();
	if (verde_R() && verde_L()){
	destravar();
	frente(110,110);
	await Time.Delay(300);
	direita(300,300);
	await Time.Delay(3624);
	frente(110,110);
	await Time.Delay(300);
	continue;
}
	if (verde_L()){
	destravar();
	frente(110,110);
	await Time.Delay(900);
	esquerda(300,300);
	await Time.Delay(1622);
	frente(110,110);
	await Time.Delay(300);
	continue;
}

	if (verde_R()){
	destravar();
	frente(110,110);
	await Time.Delay(900);
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
	direita(300,300);
	await Time.Delay(1622);
	frente(-200,-200);
	await Time.Delay(200);
	continue;
}
	continue;
}


/*	if (cor_L == "Branco" && cor_M == "Preto" && cor_R == "Preto" || cor_R2 == "Preto"){
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
	direita(300,300);
	await Time.Delay(1592);
	frente(100,100);
	await Time.Delay(100);
	continue;
}
	continue;
}*/

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
	esquerda(300,300);
	await Time.Delay(1622);
	frente(-200,-200);
	await Time.Delay(200);
	continue;
}
	continue;
}/*
if (cor_L == "Preto" || cor_L2 == "Preto" && cor_M == "Preto" && cor_R == "Branco"){
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
	esquerda(300,300);
	await Time.Delay(1592);
	frente(100,100);
	await Time.Delay(100);
	continue;
}
	continue;
}

if (cor_L == "Preto" || cor_L2 == "Preto" && cor_M == "Branco" && cor_R == "Branco"){
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
	esquerda(300,300);
	await Time.Delay(1592);
	frente(100,100);
	await Time.Delay(100);
	continue;
}
	continue;
}

if (cor_L == "Branco" && cor_M == "Branco" && cor_R == "Preto" || cor_R2 == "Preto"){
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
	direita(300,300);
	await Time.Delay(1592);
	frente(100,100);
	await Time.Delay(100);
	continue;
}
	continue;
}*/
if (cor_L == "Preto" && cor_M == "Preto" && cor_R == "Preto"){
	frente(150, 150);
	await Time.Delay(600);
}	
/*if (cor_L == "Preto" || cor_L2 == "Preto" && cor_M == "Preto" && cor_R == "Preto" || cor_R2 == "Preto"){
	frente(150, 150);
	await Time.Delay(1000);
}
if (cor_L == "Preto" && cor_M == "Preto" && cor_R == "Preto"){
	frente(150, 150);
	await Time.Delay(1000);
}*/










else {
frente(200,200);
}


/* if(virarD = true){
destravar();
direita(200,200);
await Time.Delay(400);
virarL = false;


}

if(virarL = true){
destravar();
esquerda(200,200);
await Time.Delay(400);
virarD = false;


}
else {
destravar();
frente(200, 200);
await Time.Delay(400);
}


*/



}

while(resgate == true){
await Time.Delay(50);
IO.PrintLine ("Resgate");
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
/*distanciaG2 = ultra_G2.Analog;
distanciaG3 = ultra_G3.Analog;*/








if(verde_A() && resgate == true){
	destravar();
	destravarB();
	levantarB(-300,-300);
	await Time.Delay(300);
	frente(130,130);
	await Time.Delay(1750);
	direita(300,300);
	await Time.Delay(1822);
	frente(-300,-300);
	await Time.Delay(2050);
	frente(160,160);
	await Time.Delay(5050);
	travar();
	destravarG();
	await Time.Delay(100);
	levantarG(320,320);
	await Time.Delay(900);
	levantarG(-320,-320);
	await Time.Delay(400);
	travarG();
	destravar();
	frente(110,110);
	await Time.Delay(300);
	destravarB();
	levantarB(300,300);
	await Time.Delay(300);


	// resgate das vítimas
    while(true){
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
sensores_cor();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

if(distanciaD >3 && distanciaD <7 && resgate == true){
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
sensores_cor();
frente(100,100);
await Time.Delay(100);
direita(250, 250);
await Time.Delay(2280);
frente(-200, -200);
await Time.Delay(200);

if(distanciaG <= 3 && distanciaG >= 1 && cor_B == "Branco" || cor_B2 == "Branco"){
IO.PrintLine ("fake natty!");
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
sensores_cor();
frente(150,150);
await Time.Delay(200);
destravarB();
levantarB(-250,-250);
await Time.Delay(300);
destravarM();
levantarM(400, 400);
await Time.Delay(660);
levantarM(-250, -250);
await Time.Delay(760);
levantarB(150,150);
await Time.Delay(1600);
levantarM(400, 400);
await Time.Delay(560);
levantarM(-250, -250);
await Time.Delay(560);
travarM();

}



}


if(verde_A() && resgate == true){
distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
distanciadireita = Bot.GetComponent<UltrasonicSensor>("ultra_D").Analog.ToString();
distanciagarra = Bot.GetComponent<UltrasonicSensor>("ultra_G").Analog.ToString();
distancia = ultra_F.Analog;
distanciaG = ultra_G.Analog;
distanciaD = ultra_D.Analog;
sensores_cor();
	destravar();
	destravarB();
	levantarB(-300,-300);
	await Time.Delay(300);
	frente(140,140);
	await Time.Delay(1750);
	direita(300,300);
	await Time.Delay(1822);
	frente(-300,-300);
	await Time.Delay(2050);
	frente(160,160);
	await Time.Delay(5050);
	travar();
	destravarG();
	await Time.Delay(100);
	levantarG(320,320);
	await Time.Delay(900);
	levantarG(-320,-320);
	await Time.Delay(400);
	travarG();
	destravar();
	frente(110,110);
	await Time.Delay(300);
	destravarB();
	levantarB(300,300);
	await Time.Delay(300);
	}
 
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


/*davoid sensores_ultra(){
	distanciafrente = Bot.GetComponent<UltrasonicSensor>("ultra_F").Analog.ToString();
}*/




/*bool isGreen(ColorSensor sensor, double margin=0.9) {
    Color reading = sensor.Analog;
    double real = (reading.Green * margin);
    return ((real>reading.Red) && (real>reading.Blue));
}*/

/*bool curva_d(){
direita(200, 200);


}*/

/*void right_B(){
if(cor_L == "Branco" && cor_M == "Branco" || cor_M == "Preto" && cor_R == "Preto"){
virarD = true;


/*direita(500, 500);
await Time.Delay(1000);
*/
/*}
}

void left_B(){
if(cor_L == "Preto" && cor_M == "Branco" || cor_M == "Preto" && cor_R == "Branco"){
virarL = true;


}
}*/