using Microsoft.EntityFrameworkCore.Migrations;

namespace licenta.Migrations
{
    public partial class SeedQuestionsAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Question",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NumberOfQuestions", "NumberOfWrongQuestions", "Time" },
                values: new object[] { 1, "A, A1, A2, AM", 20, 4, 20 });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "CategoryId", "Explanation", "ImagePath", "IsDisputed", "Text" },
                values: new object[,]
                {
                    { 25, 1, "Semnalul adresat de către polițistul rutier prin rotirea vioaie a brațului, semnifică mărirea vitezei de deplasare a vehiculelor sau grăbirea traversării drumului de către pietoni.	", null, false, "Rotirea vioaie a braţului poliţistului semnifică:" },
                    { 24, 1, "Motociclistul din imagine vrea să schimbe direcţia de mers către stânga, acesta s-a încadrat corect pe banda de viraj la stânga și trebuie să respecte semnalul luminos al semaforului instalat pe consolă.", "/Images/QuestionImage/6.png", false, "Motociclistul care vrea să schimbe direcţia de mers trebuie să respecte:" },
                    { 23, 1, "Pentru a conduce ecologic un autovehiculul trebuie să luați toate măsurile necesare pentru a reduce consumul de combustibil. Prin urmare, pentru a avea un consum optim de carburant, este necesar să verificaţi presiunea pneurilor o dată pe lună, la rece, altfel spus atunci când vehiculul nu a circulat mai mult de 2 ore. Pentru a vă asigura că presiunea aerului este adecvată, nu vă raportaţi la indicaţiile scrise chiar pe pneu, pentru că acestea arata presiunea maximă. Este preferabil să consultaţi manualul producatorului maşinii dumneavoastră. Pentru a conduce ecologic un autovehiculul trebuie să luați toate măsurile necesare pentru a reduce poluarea mediului înconjurător. Prin urmare, pentru a reducere poluarea mediului, este necesar să schimbaţi între ele pneurile (permutaţi) la fiecare 10.000 de km şi efectuaţi reglajul geometriei şi echilibrarea roţilor o dată pe an, lucru care duce la uzarea uniformă a pneurilor. Această masură va crește durata de viață a anvelopelor și astfel se va reduce poluarea.", null, false, "Pentru a conduce ecologic un autovehiculul, este necesar:" },
                    { 22, 1, "Se interzice staționarea vehiculelor în toate cazurile în care este interzisă oprirea voluntară a vehiculelor, iar oprirea voluntară vă este interzisă pe partea carosabilă a drumurilor național europene (E) și bineînțeles vă este interzisă și staționarea.	", null, false, "Când circulaţi pe un drum naţional european vă este interzisă:" },
                    { 21, 1, "Imobilizarea unui vehicul de către polițistul rutier se dispune pentru refuzul conducătorului de a se legitima. Nu se imobilizează vehiculul pentru lipsa triunghiurilor reflectorizante sau pentru lipsa trusei medicale de prim ajutor.", null, false, "Imobilizarea unei motociclete de către poliţistul rutier se dispune:" },
                    { 20, 1, "Pe drumurile naționale europene din afara localităților, limita maximă de viteză cu care aveți voie să conduceți un autovehicul aparținând categoriei A este de 100km/h.", null, false, "Care este limita maximă de viteză cu care aveţi voie să conduceţi, pe drumurile naţionale europene din afara localităţilor, un autovehicul aparţinând categoriei A?	" },
                    { 19, 1, "Pe drumurile naționale europene din afara localităților,  limita maximă de viteză pentru un autovehicul aparținând subcategoriei A1 este de 80km/h.", null, false, "Care este limita maximă de viteză cu care aveţi voie să conduceţi, pe drumurile naţionale europene din localităţi, un autovehicul aparţinând subcategoriei A1?" },
                    { 18, 1, "Participanții la trafic sunt obligați să oprească la semnalele adresate de poliţiştii de frontieră circulație, îndrumătorii de circulaţie ai Ministerului Apărării, agenţii de cale ferată, la trecerile la nivel, personalul autorizat din zona lucrărilor pe drumurile publice, membrii patrulelor şcolare de circulaţie, la trecerile pentru pietoni din apropierea unităţilor de învățământ, precum și ale nevăzătorilor, prin ridicarea bastonului alb atunci când aceştia traversează strada. NU aveți obligația să opriți la semnalele adresate de poliţiştii comunitari și personalul autorizat din cadrul serviciului examinări.", null, false, "Aveţi obligaţia de a opri imediat motocicleta la semnalele adresate de:" },
                    { 17, 1, "Indicaorul 1 se numește ,,Înainte”, aces indicator obligăconducătorii devehicule să crcule pe direcție înainte în prime întersecție. Indicatorul 2 se  numește ,,Sens unic”, acest indicator are rolul dea informa conducătorii de vehicule că drumul pe care circulă este un drum cu sens unic.", "/Images/QuestionImage/4.png", false, "Ce indicator arată că circulaţia se desfăşoară într-un singur sens?" },
                    { 16, 1, "Este interzis conducătorilor de autovehicule să amenințe participanții la trafic cu violențe, să săvârșească acte sau gesturi obscene sau să adreseze celorlalți particpanți la trafic expresii jignitoare sau vulgare.", null, false, "Conducătorilor de motociclete le este interzis:	" },
                    { 15, 1, "Legislația din România nu obligă la oprire în această situație. Dacă șoferul autobuzului școlar pune în funcțiune luminile deavarie, o face pentru a avertiza cillți particpanți la trafic că acesta constituie un pericol pentru ceilalți participanți la trafic, iar din punct de vdere al conduitei preventive se recomandă reducerea vitezei și sporirea atenției.", null, false, "Cum trebuie să procedaţi atunci când un autobuz şcolar opreşte, cu luminile intermitente de avertizare aprinse?	" },
                    { 14, 1, "Într-o intersecție dirijată cu semanle luminoase în care unt instalate și indicatoare de reglare de prioritate, conducătorul de vehicul este obligat să respecte semnalele luminoase cu prioritate de față de indicatoarele de reglare de prioritate.", null, false, "Semnificaţia indicatoarelor „Oprire” şi „Cedează trecerea”, instalate în intersecţiile semaforizate, trebuie respectată" },
                    { 13, 1, "Conducătorul de autovehicul este obligat să permită controlul stării tehnice a vehiculului, precum și al bunurilor transportate. Această verificare a stării tehnice avehiculelor aflate în trafic pe drumurile publice se face de către poliția rutieră, împreună cu instituțiile abilitate de lege și să se prezinte la verificarea medicală periodică, potrivit legii.", null, false, "Conducătorul unei motociclete este obligat:	" },
                    { 12, 1, "Intesecția din imagine este dirijată prin indicatoare de reglementare de prioritate. Autocamionul vine de pe drumul fără prioritate și trebuie să îi acorde prioritate de trecere motocicletei și autoturismului. Între autoturism și motocicletă s-a crea o situație de egalitate, situație în cre se va aplica regula priorității de dreapta, deci în această situație motocicleta îi va acorda prioritate autoturismului deoarece virând către stânga autoturismul îi va veni din partea dreaptă.", "/Images/QuestionImage/3.png", false, "Care este ordinea de intrare în intersecţie?" },
                    { 11, 1, "Vitezele maxime în afara localităților pentru categoriile și subgategoriile de autovehicule prevăzute la  art. 15 alin. (2) sunt următoarele: 130km/h pe autostrăzi, 100km/h pe drumurile expres sau pe cele naționle europene (E) și 90 km/h pe celelalte categorii de drummuri, pentru autovehiculele din categoriile A și B.", null, false, "Care este limita maximă de viteză cu care aveţi voie să conduceţi un autovehicul aparţinând categoriei A, pe autostrăzi?" },
                    { 10, 1, "Pe timpul nopții la apropierea de un alt vehicul care circulă din sens opus, sunteți obligat ca de la distanța de cel putin 200 de metri să folosiți luminile de întalnire concomitent cu reducerea vitezei.", null, false, "Vă apropiaţi de o trecere peste calea ferată şi observaţi că se coboară semibariera. Cum procedaţi?	" },
                    { 9, 1, "Pe timpul nopții, la apropierea de un alt vehicul care circulă din sens opus, sunteți obligat ca de la distanța de cel puțin 200 de metri să folosiți luminile de întâlnire concomitent cu reducerea vitezei.", null, false, "De la ce distanţă faţă de vehiculul care circulă din sens opus, pe timp de noapte, conducătorul unei motociclete este obligat să folosească lumina de întâlnire?" },
                    { 8, 1, "Conducătorului auto îi este interzis să circule cu autovehiculul avariat mai mult de 30 de zile de la producerea avarierei.	", null, false, "Aveţi posibilitatea de a circula pe drumurile publice cu motocicleta avariată:" },
                    { 7, 1, "La apropierea de o trecere la nivel cu o cale ferată prevăzută cu semibariere automate, semnalele sonore și luminile roșii fiind în funcțiune, aveți obligația de a reduce viteza și de a opri autovehiculul înaintea semibarierelor.	", null, false, "Cum veţi proceda corect dacă, la apropierea de o trecere la nivel cu o cale ferată prevăzută cu semibariere automate, luminile roşii şi semnalele sonore sunt în funcţiune?	" },
                    { 6, 1, "some", null, false, "Într-o intersecţie unde circulaţia se desfăşoară în sens giratoriu, conducătorului unei motociclete îi este interzis:" },
                    { 5, 1, "Cand semnalul de culoare galbena apare dupa semnalul de culoare verde, conducatorul vehiculului care se apropie de intersectie nu trebuie sa treaca de locurile prevazute la art. 52 alin. (2), cu exceptia situatiei in care, la aparitia semnalului, se afla atat de aproape de acele locuri incat nu ar mai putea opri vehiculul in conditii de siguranta.", null, false, "Semnalul galben al semaforului:" },
                    { 4, 1, "Substanțele antiseptice sunt substanțe dezinfectante care previn ori înlăturarea infecțiile microbiene. Acestea se folosesc în dezinfectarea rănilor.", null, false, "Substanţele antiseptice se folosesc pentru:	" },
                    { 3, 1, "Indicatorul pe care l-ați întâlnit se numește “Curbă deosebit de periculoasă”. Acest indicator ne avertizează că urmează o curbă deosebit de periculoasă către dreapta. La întâlnirea lui se impune reducerea vitezei de deplasare la 30 de km/h în localități și 50 de km/h în afara localităților, toate manevrele (depășirea, oprirea, staționarea, mersul înapoi și întoarcerea) fiind interzise.", "/Images/QuestionImage/2.png", false, "Ce trebuie să faceţi când întâlniţi indicatorul din imagine?" },
                    { 2, 1, "Dacă ați fost implicat într-un accident de circulație în urmă căruia a rezultat moartea sau vătămarea integrității corporale ori a sănătații unei persoane sunteți obligat să anunțați imediat poliția la numărul naţional unic pentru apeluri de urgenţă 112, să nu modificați sau să ștergeti urmele accidentului și să nu parasiți locul faptei. Dacă ați fost implicat într-un accident de circulație în urmă căruia a rezultat moartea sau vătămarea integrității corporale ori a sănătații unei persoane sunteți obligat să anunțați imediat poliția la numărul naţional unic pentru apeluri de urgenţă 112, să nu modificați sau să ștergeti urmele accidentului și să nu parasiți locul faptei.", null, false, "În cazul implicării într-un accident, puteţi părăsi locul acestuia fără încuviinţarea poliţiei, dacă a rezultat vătămarea integrităţii corporale a unei persoane?" },
                    { 1, 1, "Panourile din imagine se numesc ,,Panouri suplimentare la nodurile rutiere de pe autostrăzi”. Aceste panouri se găsesc instalate după cum urmează: primul, cel cu trei linii, la 300 de metri; al doilea, cel cu doua linii la 200 de metri si al treilea; cel cu o linie la 100 de metri de o iesire de pe autostrada.", "/Images/QuestionImage/1.png", false, "Ce semnificaţie au panourile alăturate?" },
                    { 26, 1, "La apropierea de o trecere la nivel cu o cale ferată prevazută cu semibariere automate, semnalele sonore și luminile roșii fiind în funcțiune, aveți obligația de a reduce viteza și de a opri autovehiculul înaintea semibarierelor.	", null, false, "Cum veţi proceda corect dacă, la apropierea de o trecere la nivel cu o cale ferată prevăzută cu semibariere automate, luminile roşii şi semnalele sonore sunt în funcţiune?	" },
                    { 27, 1, "Conducătorului de autovehicul îi este interzis să circule cu autovehiculul avariat mai mult de 30 de zile de la producerea avariei.", null, false, "Aveţi posibilitatea de a circula pe drumurile publice cu motocicleta avariată:	" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, true, 1, "panouri suplimentare la nodurile rutiere de pe autostrăzi;" },
                    { 61, false, 20, "70 km/h;" },
                    { 60, false, 19, "100 km/h;" },
                    { 59, true, 19, "80 km/h;" },
                    { 58, false, 19, "50 km/h;" },
                    { 57, true, 18, "personalul autorizat din zona lucrărilor pe drumurile publice.	" },
                    { 56, false, 18, "personalul autorizat din cadrul serviciului examinări;	" },
                    { 55, false, 18, "poliţiştii comunitari;	" },
                    { 62, false, 20, "90 km/h;" },
                    { 54, false, 17, "niciunul." },
                    { 52, false, 17, "indicatorul 1;" },
                    { 51, true, 16, "să adreseze celorlalţi participanţi la trafic expresii jignitoare sau vulgare.	" },
                    { 50, true, 16, "să săvârşească acte sau gesturi obscene;" },
                    { 49, true, 16, "să ameninţe participanţii la trafic cu violenţe;" },
                    { 48, true, 15, "circulaţi cu atenţie sporită;" },
                    { 47, false, 15, "opriţi până când acesta îşi începe din nou deplasarea;	" },
                    { 46, false, 15, "nu aveţi nicio obligaţie, deoarece semnalizarea nu vi se adresează;" },
                    { 53, true, 17, "indicatorul 2;" },
                    { 63, true, 20, "100 km/h." },
                    { 64, false, 21, "pentru lipsa triunghiurilor reflectorizante;" },
                    { 65, false, 21, "pentru lipsa trusei medicale de prim ajutor;" },
                    { 82, false, 27, "cel mult 10 zile de la data constatării evenimentului;	" },
                    { 81, false, 26, "măriţi viteza pentru a nu fi surprinşi de închiderea semibarierei;	" },
                    { 80, false, 26, "ocoliţi semibariera şi vă continuaţi drumul, dacă trenul nu se apropie;" },
                    { 79, true, 26, "reduceţi viteza şi opriţi înaintea semibarierelor;	" },
                    { 78, false, 25, "reducerea vitezei de deplasare;" },
                    { 77, false, 25, "oprirea temporară a circulaţiei;" },
                    { 76, true, 25, "mărirea vitezei de deplasare sau grăbirea traversării drumului de către pietoni;	" },
                    { 75, false, 24, "semnalul luminos al oricăruia dintre semafoare;" },
                    { 74, true, 24, "semnalul luminos al semaforului instalat pe consolă;" },
                    { 73, false, 24, "semnalul luminos al semaforului instalat pe trotuar;" },
                    { 72, true, 23, "să înlocuiţi pneurile la fiecare 1000 km." },
                    { 71, true, 23, "să schimbaţi între ele pneurile, la aproximativ 10 000 km;	" },
                    { 70, true, 23, "să verificaţi o dată pe lună, la rece, presiunea din pneuri;" },
                    { 69, true, 22, "staţionarea voluntară pe partea carosabilă;" },
                    { 68, false, 22, "remorcarea altui autovehicul;" },
                    { 67, false, 22, "folosirea mijloacelor de avertizare sonoră;" },
                    { 66, true, 21, "pentru refuzul de legitimare;" },
                    { 45, false, 14, "atunci când indicatoarele sunt instalate împreună cu semaforul pe acelaşi suport;" },
                    { 83, false, 27, "până la expirarea asigurării obligatorii de răspundere civilă auto;" },
                    { 44, true, 14, "atunci când semafoarele nu funcţionează ;" },
                    { 42, false, 13, "să transporte poliţişti, la cererea acestora;" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 17, true, 6, "să oprească motocicleta, în afara situaţiilor impuse de trafic;" },
                    { 16, false, 6, "să reducă viteza, întrucât se creează pericolul de blocare;" },
                    { 15, true, 5, "interzice intrarea în intersecţie, dacă motocicleta poate fi oprită în condiţii de siguranţă;" },
                    { 14, false, 5, "permite schimbarea direcţiei de mers către dreapta;" },
                    { 13, false, 5, "permite intrarea în intersecţie, dacă urmează după lumina verde;" },
                    { 12, false, 4, "oprirea hemoragiei." },
                    { 11, false, 4, "calmarea durerilor;" },
                    { 18, false, 6, "să întoarcă, prin ocolirea sensului giratoriu;	" },
                    { 10, true, 4, "dezinfectarea rănilor;	" },
                    { 8, true, 3, "reduceţi viteza şi circulaţi cu atenţie, deoarece intraţi într-o curbă deosebit de periculoasă;	" },
                    { 7, false, 3, "semnalizaţi înscrierea în curbă prin mijloace specifice;	" },
                    { 6, true, 2, "nu, întrucât săvârşiţi o infracţiune;" },
                    { 5, false, 2, "da, în cazul în care motocicleta dvs. nu a fost avariată;" },
                    { 4, false, 2, "da, dacă accidentul nu s-a produs din vina dvs.;" },
                    { 3, false, 1, "panouri succesive la apropierea de drumurile europene;" },
                    { 2, false, 1, "panouri suplimentare pentru intersecţia cu calea ferată;" },
                    { 9, false, 3, "semnalizaţi sonor intrarea în curbă;" },
                    { 19, true, 7, "reduceţi viteza şi opriţi înaintea semibarierelor;	" },
                    { 20, false, 7, "ocoliţi semibariera şi vă continuaţi drumul, dacă trenul nu se apropie;" },
                    { 21, false, 7, "măriţi viteza pentru a nu fi surprinşi de închiderea semibarierei;	" },
                    { 40, true, 13, "să se prezinte la verificarea medicală periodică, potrivit legii	" },
                    { 38, true, 13, "să permită controlul stării tehnice a motocicletei, precum şi al bunurilor transportate, în condiţiile legii" },
                    { 37, false, 12, "motocicleta, autoturismul, autocamionul." },
                    { 36, true, 12, " autoturismul, motocicleta, autocamionul;" },
                    { 35, false, 12, "autoturismul, autocamionul, motocicleta;" },
                    { 31, false, 11, "90 km/h;" },
                    { 30, true, 11, "130 km/h;" },
                    { 29, false, 11, "100 km/h;" },
                    { 34, false, 10, "dacă nu se apropie trenul, ocoliţi semibariera;" },
                    { 33, true, 10, "opriţi în faţa semibarierei;" },
                    { 32, false, 10, "acceleraţi şi treceţi cât mai repede peste calea ferată;" },
                    { 28, true, 9, "de la cel puţin 200 m;	" },
                    { 27, false, 9, "de la cel puţin 150 m;	" },
                    { 26, false, 9, "de la cel puţin 100 m;	" },
                    { 25, true, 8, "cel mult 30 zile de la data producerii avariei;" },
                    { 24, false, 8, "până la expirarea asigurării obligatorii de răspundere civilă auto;" },
                    { 22, false, 8, "cel mult 10 zile de la data constatării evenimentului;	" },
                    { 43, false, 14, " atunci când traficul este redus;" },
                    { 84, true, 27, "cel mult 30 zile de la data producerii avariei;" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Question",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
