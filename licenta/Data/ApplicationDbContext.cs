using licenta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<Question> Question { get; set; }
		public DbSet<Test> Tests { get; set; }

		public DbSet<Message> Messages { get; set; }
		public DbSet<Connection> Connections { get; set; }
		public DbSet<Note> Notes { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Category>().HasData(
					new Category { Id = 1, Name = "A, A1, A2, AM", NumberOfQuestions = 20, NumberOfWrongQuestions = 4, Time = 20 }
				);

			builder.Entity<Question>().HasData(
					new Question { Id = 1, Text = "Ce semnificaţie au panourile alăturate?", ImagePath = "/Images/QuestionImage/1.png", Explanation = "Panourile din imagine se numesc ,,Panouri suplimentare la nodurile rutiere de pe autostrăzi”. Aceste panouri se găsesc instalate după cum urmează: primul, cel cu trei linii, la 300 de metri; al doilea, cel cu doua linii la 200 de metri si al treilea; cel cu o linie la 100 de metri de o iesire de pe autostrada.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 2, Text = "În cazul implicării într-un accident, puteţi părăsi locul acestuia fără încuviinţarea poliţiei, dacă a rezultat vătămarea integrităţii corporale a unei persoane?", IsDisputed = false, Explanation = "Dacă ați fost implicat într-un accident de circulație în urmă căruia a rezultat moartea sau vătămarea integrității corporale ori a sănătații unei persoane sunteți obligat să anunțați imediat poliția la numărul naţional unic pentru apeluri de urgenţă 112, să nu modificați sau să ștergeti urmele accidentului și să nu parasiți locul faptei. Dacă ați fost implicat într-un accident de circulație în urmă căruia a rezultat moartea sau vătămarea integrității corporale ori a sănătații unei persoane sunteți obligat să anunțați imediat poliția la numărul naţional unic pentru apeluri de urgenţă 112, să nu modificați sau să ștergeti urmele accidentului și să nu parasiți locul faptei.", CategoryId = 1 },
					new Question { Id = 3, Text = "Ce trebuie să faceţi când întâlniţi indicatorul din imagine?", Explanation = "Indicatorul pe care l-ați întâlnit se numește “Curbă deosebit de periculoasă”. Acest indicator ne avertizează că urmează o curbă deosebit de periculoasă către dreapta. La întâlnirea lui se impune reducerea vitezei de deplasare la 30 de km/h în localități și 50 de km/h în afara localităților, toate manevrele (depășirea, oprirea, staționarea, mersul înapoi și întoarcerea) fiind interzise.", ImagePath = "/Images/QuestionImage/2.png", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 4, Text = "Substanţele antiseptice se folosesc pentru:	", IsDisputed = false, Explanation = "Substanțele antiseptice sunt substanțe dezinfectante care previn ori înlăturarea infecțiile microbiene. Acestea se folosesc în dezinfectarea rănilor.", CategoryId = 1 },
					new Question { Id = 5, Text = "Semnalul galben al semaforului:", IsDisputed = false, Explanation = "Cand semnalul de culoare galbena apare dupa semnalul de culoare verde, conducatorul vehiculului care se apropie de intersectie nu trebuie sa treaca de locurile prevazute la art. 52 alin. (2), cu exceptia situatiei in care, la aparitia semnalului, se afla atat de aproape de acele locuri incat nu ar mai putea opri vehiculul in conditii de siguranta.", CategoryId = 1 },
					new Question { Id = 6, Text = "Într-o intersecţie unde circulaţia se desfăşoară în sens giratoriu, conducătorului unei motociclete îi este interzis:", Explanation = "some", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 7, Text = "Cum veţi proceda corect dacă, la apropierea de o trecere la nivel cu o cale ferată prevăzută cu semibariere automate, luminile roşii şi semnalele sonore sunt în funcţiune?	", Explanation = "La apropierea de o trecere la nivel cu o cale ferată prevăzută cu semibariere automate, semnalele sonore și luminile roșii fiind în funcțiune, aveți obligația de a reduce viteza și de a opri autovehiculul înaintea semibarierelor.	", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 8, Text = "Aveţi posibilitatea de a circula pe drumurile publice cu motocicleta avariată:", Explanation = "Conducătorului auto îi este interzis să circule cu autovehiculul avariat mai mult de 30 de zile de la producerea avarierei.	", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 9, Text = "De la ce distanţă faţă de vehiculul care circulă din sens opus, pe timp de noapte, conducătorul unei motociclete este obligat să folosească lumina de întâlnire?", IsDisputed = false, Explanation = "Pe timpul nopții, la apropierea de un alt vehicul care circulă din sens opus, sunteți obligat ca de la distanța de cel puțin 200 de metri să folosiți luminile de întâlnire concomitent cu reducerea vitezei.", CategoryId = 1 },
					new Question { Id = 10, Text = "Vă apropiaţi de o trecere peste calea ferată şi observaţi că se coboară semibariera. Cum procedaţi?	", Explanation = "Pe timpul nopții la apropierea de un alt vehicul care circulă din sens opus, sunteți obligat ca de la distanța de cel putin 200 de metri să folosiți luminile de întalnire concomitent cu reducerea vitezei.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 11, Text = "Care este limita maximă de viteză cu care aveţi voie să conduceţi un autovehicul aparţinând categoriei A, pe autostrăzi?", Explanation = "Vitezele maxime în afara localităților pentru categoriile și subgategoriile de autovehicule prevăzute la  art. 15 alin. (2) sunt următoarele: 130km/h pe autostrăzi, 100km/h pe drumurile expres sau pe cele naționle europene (E) și 90 km/h pe celelalte categorii de drummuri, pentru autovehiculele din categoriile A și B.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 12, Text = "Care este ordinea de intrare în intersecţie?", Explanation = "Intesecția din imagine este dirijată prin indicatoare de reglementare de prioritate. Autocamionul vine de pe drumul fără prioritate și trebuie să îi acorde prioritate de trecere motocicletei și autoturismului. Între autoturism și motocicletă s-a crea o situație de egalitate, situație în cre se va aplica regula priorității de dreapta, deci în această situație motocicleta îi va acorda prioritate autoturismului deoarece virând către stânga autoturismul îi va veni din partea dreaptă.", ImagePath = "/Images/QuestionImage/3.png", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 13, Text = "Conducătorul unei motociclete este obligat:	", Explanation = "Conducătorul de autovehicul este obligat să permită controlul stării tehnice a vehiculului, precum și al bunurilor transportate. Această verificare a stării tehnice avehiculelor aflate în trafic pe drumurile publice se face de către poliția rutieră, împreună cu instituțiile abilitate de lege și să se prezinte la verificarea medicală periodică, potrivit legii.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 14, Text = "Semnificaţia indicatoarelor „Oprire” şi „Cedează trecerea”, instalate în intersecţiile semaforizate, trebuie respectată", Explanation = "Într-o intersecție dirijată cu semanle luminoase în care unt instalate și indicatoare de reglare de prioritate, conducătorul de vehicul este obligat să respecte semnalele luminoase cu prioritate de față de indicatoarele de reglare de prioritate.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 15, Text = "Cum trebuie să procedaţi atunci când un autobuz şcolar opreşte, cu luminile intermitente de avertizare aprinse?	", Explanation = "Legislația din România nu obligă la oprire în această situație. Dacă șoferul autobuzului școlar pune în funcțiune luminile deavarie, o face pentru a avertiza cillți particpanți la trafic că acesta constituie un pericol pentru ceilalți participanți la trafic, iar din punct de vdere al conduitei preventive se recomandă reducerea vitezei și sporirea atenției.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 16, Text = "Conducătorilor de motociclete le este interzis:	", Explanation = "Este interzis conducătorilor de autovehicule să amenințe participanții la trafic cu violențe, să săvârșească acte sau gesturi obscene sau să adreseze celorlalți particpanți la trafic expresii jignitoare sau vulgare.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 17, Text = "Ce indicator arată că circulaţia se desfăşoară într-un singur sens?", Explanation = "Indicaorul 1 se numește ,,Înainte”, aces indicator obligăconducătorii devehicule să crcule pe direcție înainte în prime întersecție. Indicatorul 2 se  numește ,,Sens unic”, acest indicator are rolul dea informa conducătorii de vehicule că drumul pe care circulă este un drum cu sens unic.", ImagePath = "/Images/QuestionImage/4.png", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 18, Text = "Aveţi obligaţia de a opri imediat motocicleta la semnalele adresate de:", Explanation = "Participanții la trafic sunt obligați să oprească la semnalele adresate de poliţiştii de frontieră circulație, îndrumătorii de circulaţie ai Ministerului Apărării, agenţii de cale ferată, la trecerile la nivel, personalul autorizat din zona lucrărilor pe drumurile publice, membrii patrulelor şcolare de circulaţie, la trecerile pentru pietoni din apropierea unităţilor de învățământ, precum și ale nevăzătorilor, prin ridicarea bastonului alb atunci când aceştia traversează strada. NU aveți obligația să opriți la semnalele adresate de poliţiştii comunitari și personalul autorizat din cadrul serviciului examinări.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 19, Text = "Care este limita maximă de viteză cu care aveţi voie să conduceţi, pe drumurile naţionale europene din localităţi, un autovehicul aparţinând subcategoriei A1?", Explanation = "Pe drumurile naționale europene din afara localităților,  limita maximă de viteză pentru un autovehicul aparținând subcategoriei A1 este de 80km/h.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 20, Text = "Care este limita maximă de viteză cu care aveţi voie să conduceţi, pe drumurile naţionale europene din afara localităţilor, un autovehicul aparţinând categoriei A?	", Explanation = "Pe drumurile naționale europene din afara localităților, limita maximă de viteză cu care aveți voie să conduceți un autovehicul aparținând categoriei A este de 100km/h.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 21, Text = "Imobilizarea unei motociclete de către poliţistul rutier se dispune:", Explanation = "Imobilizarea unui vehicul de către polițistul rutier se dispune pentru refuzul conducătorului de a se legitima. Nu se imobilizează vehiculul pentru lipsa triunghiurilor reflectorizante sau pentru lipsa trusei medicale de prim ajutor.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 22, Text = "Când circulaţi pe un drum naţional european vă este interzisă:", Explanation = "Se interzice staționarea vehiculelor în toate cazurile în care este interzisă oprirea voluntară a vehiculelor, iar oprirea voluntară vă este interzisă pe partea carosabilă a drumurilor național europene (E) și bineînțeles vă este interzisă și staționarea.	", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 23, Text = "Pentru a conduce ecologic un autovehiculul, este necesar:", Explanation = "Pentru a conduce ecologic un autovehiculul trebuie să luați toate măsurile necesare pentru a reduce consumul de combustibil. Prin urmare, pentru a avea un consum optim de carburant, este necesar să verificaţi presiunea pneurilor o dată pe lună, la rece, altfel spus atunci când vehiculul nu a circulat mai mult de 2 ore. Pentru a vă asigura că presiunea aerului este adecvată, nu vă raportaţi la indicaţiile scrise chiar pe pneu, pentru că acestea arata presiunea maximă. Este preferabil să consultaţi manualul producatorului maşinii dumneavoastră. Pentru a conduce ecologic un autovehiculul trebuie să luați toate măsurile necesare pentru a reduce poluarea mediului înconjurător. Prin urmare, pentru a reducere poluarea mediului, este necesar să schimbaţi între ele pneurile (permutaţi) la fiecare 10.000 de km şi efectuaţi reglajul geometriei şi echilibrarea roţilor o dată pe an, lucru care duce la uzarea uniformă a pneurilor. Această masură va crește durata de viață a anvelopelor și astfel se va reduce poluarea.", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 24, Text = "Motociclistul care vrea să schimbe direcţia de mers trebuie să respecte:", Explanation = "Motociclistul din imagine vrea să schimbe direcţia de mers către stânga, acesta s-a încadrat corect pe banda de viraj la stânga și trebuie să respecte semnalul luminos al semaforului instalat pe consolă.", ImagePath = "/Images/QuestionImage/6.png", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 25, Text = "Rotirea vioaie a braţului poliţistului semnifică:", Explanation = "Semnalul adresat de către polițistul rutier prin rotirea vioaie a brațului, semnifică mărirea vitezei de deplasare a vehiculelor sau grăbirea traversării drumului de către pietoni.	", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 26, Text = "Cum veţi proceda corect dacă, la apropierea de o trecere la nivel cu o cale ferată prevăzută cu semibariere automate, luminile roşii şi semnalele sonore sunt în funcţiune?	", Explanation = "La apropierea de o trecere la nivel cu o cale ferată prevazută cu semibariere automate, semnalele sonore și luminile roșii fiind în funcțiune, aveți obligația de a reduce viteza și de a opri autovehiculul înaintea semibarierelor.	", IsDisputed = false, CategoryId = 1 },
					new Question { Id = 27, Text = "Aveţi posibilitatea de a circula pe drumurile publice cu motocicleta avariată:	", Explanation = "Conducătorului de autovehicul îi este interzis să circule cu autovehiculul avariat mai mult de 30 de zile de la producerea avariei.", IsDisputed = false, CategoryId = 1 }
			);


			builder.Entity<Answer>().HasData(
					new Answer { Id = 1, Text = "panouri suplimentare la nodurile rutiere de pe autostrăzi;", IsCorrect = true, QuestionId = 1 },
					new Answer { Id = 2, Text = "panouri suplimentare pentru intersecţia cu calea ferată;", IsCorrect = false, QuestionId = 1 },
					new Answer { Id = 3, Text = "panouri succesive la apropierea de drumurile europene;", IsCorrect = false, QuestionId = 1 },
					new Answer { Id = 4, Text = "da, dacă accidentul nu s-a produs din vina dvs.;", IsCorrect = false, QuestionId = 2 },
					new Answer { Id = 5, Text = "da, în cazul în care motocicleta dvs. nu a fost avariată;", IsCorrect = false, QuestionId = 2 },
					new Answer { Id = 6, Text = "nu, întrucât săvârşiţi o infracţiune;", IsCorrect = true, QuestionId = 2 },
					new Answer { Id = 7, Text = "semnalizaţi înscrierea în curbă prin mijloace specifice;	", IsCorrect = false, QuestionId = 3 },
					new Answer { Id = 8, Text = "reduceţi viteza şi circulaţi cu atenţie, deoarece intraţi într-o curbă deosebit de periculoasă;	", IsCorrect = true, QuestionId = 3 },
					new Answer { Id = 9, Text = "semnalizaţi sonor intrarea în curbă;", IsCorrect = false, QuestionId = 3 },
					new Answer { Id = 10, Text = "dezinfectarea rănilor;	", IsCorrect = true, QuestionId = 4 },
					new Answer { Id = 11, Text = "calmarea durerilor;", IsCorrect = false, QuestionId = 4 },
					new Answer { Id = 12, Text = "oprirea hemoragiei.", IsCorrect = false, QuestionId = 4 },
					new Answer { Id = 13, Text = "permite intrarea în intersecţie, dacă urmează după lumina verde;", IsCorrect = false, QuestionId = 5 },
					new Answer { Id = 14, Text = "permite schimbarea direcţiei de mers către dreapta;", IsCorrect = false, QuestionId = 5 },
					new Answer { Id = 15, Text = "interzice intrarea în intersecţie, dacă motocicleta poate fi oprită în condiţii de siguranţă;", IsCorrect = true, QuestionId = 5 },
					new Answer { Id = 16, Text = "să reducă viteza, întrucât se creează pericolul de blocare;", IsCorrect = false, QuestionId = 6 },
					new Answer { Id = 17, Text = "să oprească motocicleta, în afara situaţiilor impuse de trafic;", IsCorrect = true, QuestionId = 6 },
					new Answer { Id = 18, Text = "să întoarcă, prin ocolirea sensului giratoriu;	", IsCorrect = false, QuestionId = 6 },
					new Answer { Id = 19, Text = "reduceţi viteza şi opriţi înaintea semibarierelor;	", IsCorrect = true, QuestionId = 7 },
					new Answer { Id = 20, Text = "ocoliţi semibariera şi vă continuaţi drumul, dacă trenul nu se apropie;", IsCorrect = false, QuestionId = 7 },
					new Answer { Id = 21, Text = "măriţi viteza pentru a nu fi surprinşi de închiderea semibarierei;	", IsCorrect = false, QuestionId = 7 },
					new Answer { Id = 22, Text = "cel mult 10 zile de la data constatării evenimentului;	", IsCorrect = false, QuestionId = 8 },
					new Answer { Id = 24, Text = "până la expirarea asigurării obligatorii de răspundere civilă auto;", IsCorrect = false, QuestionId = 8 },
					new Answer { Id = 25, Text = "cel mult 30 zile de la data producerii avariei;", IsCorrect = true, QuestionId = 8 },
					new Answer { Id = 26, Text = "de la cel puţin 100 m;	", IsCorrect = false, QuestionId = 9 },
					new Answer { Id = 27, Text = "de la cel puţin 150 m;	", IsCorrect = false, QuestionId = 9 },
					new Answer { Id = 28, Text = "de la cel puţin 200 m;	", IsCorrect = true, QuestionId = 9 },
					new Answer { Id = 29, Text = "100 km/h;", IsCorrect = false, QuestionId = 11 },
					new Answer { Id = 30, Text = "130 km/h;", IsCorrect = true, QuestionId = 11 },
					new Answer { Id = 31, Text = "90 km/h;", IsCorrect = false, QuestionId = 11 },
					new Answer { Id = 32, Text = "acceleraţi şi treceţi cât mai repede peste calea ferată;", IsCorrect = false, QuestionId = 10 },
					new Answer { Id = 33, Text = "opriţi în faţa semibarierei;", IsCorrect = true, QuestionId = 10 },
					new Answer { Id = 34, Text = "dacă nu se apropie trenul, ocoliţi semibariera;", IsCorrect = false, QuestionId = 10 },
					new Answer { Id = 35, Text = "autoturismul, autocamionul, motocicleta;", IsCorrect = false, QuestionId = 12 },
					new Answer { Id = 36, Text = " autoturismul, motocicleta, autocamionul;", IsCorrect = true, QuestionId = 12 },
					new Answer { Id = 37, Text = "motocicleta, autoturismul, autocamionul.", IsCorrect = false, QuestionId = 12 },
					new Answer { Id = 38, Text = "să permită controlul stării tehnice a motocicletei, precum şi al bunurilor transportate, în condiţiile legii", IsCorrect = true, QuestionId = 13 },
					new Answer { Id = 40, Text = "să se prezinte la verificarea medicală periodică, potrivit legii	", IsCorrect = true, QuestionId = 13 },
					new Answer { Id = 42, Text = "să transporte poliţişti, la cererea acestora;", IsCorrect = false, QuestionId = 13 },
					new Answer { Id = 43, Text = " atunci când traficul este redus;", IsCorrect = false, QuestionId = 14 },
					new Answer { Id = 44, Text = "atunci când semafoarele nu funcţionează ;", IsCorrect = true, QuestionId = 14 },
					new Answer { Id = 45, Text = "atunci când indicatoarele sunt instalate împreună cu semaforul pe acelaşi suport;", IsCorrect = false, QuestionId = 14 },
					new Answer { Id = 46, Text = "nu aveţi nicio obligaţie, deoarece semnalizarea nu vi se adresează;", IsCorrect = false, QuestionId = 15 },
					new Answer { Id = 47, Text = "opriţi până când acesta îşi începe din nou deplasarea;	", IsCorrect = false, QuestionId = 15 },
					new Answer { Id = 48, Text = "circulaţi cu atenţie sporită;", IsCorrect = true, QuestionId = 15 },
					new Answer { Id = 49, Text = "să ameninţe participanţii la trafic cu violenţe;", IsCorrect = true, QuestionId = 16 },
					new Answer { Id = 50, Text = "să săvârşească acte sau gesturi obscene;", IsCorrect = true, QuestionId = 16 },
					new Answer { Id = 51, Text = "să adreseze celorlalţi participanţi la trafic expresii jignitoare sau vulgare.	", IsCorrect = true, QuestionId = 16 },
					new Answer { Id = 52, Text = "indicatorul 1;", IsCorrect = false, QuestionId = 17 },
					new Answer { Id = 53, Text = "indicatorul 2;", IsCorrect = true, QuestionId = 17 },
					new Answer { Id = 54, Text = "niciunul.", IsCorrect = false, QuestionId = 17 },
					new Answer { Id = 55, Text = "poliţiştii comunitari;	", IsCorrect = false, QuestionId = 18 },
					new Answer { Id = 56, Text = "personalul autorizat din cadrul serviciului examinări;	", IsCorrect = false, QuestionId = 18 },
					new Answer { Id = 57, Text = "personalul autorizat din zona lucrărilor pe drumurile publice.	", IsCorrect = true, QuestionId = 18 },
					new Answer { Id = 58, Text = "50 km/h;", IsCorrect = false, QuestionId = 19 },
					new Answer { Id = 59, Text = "80 km/h;", IsCorrect = true, QuestionId = 19 },
					new Answer { Id = 60, Text = "100 km/h;", IsCorrect = false, QuestionId = 19 },
					new Answer { Id = 61, Text = "70 km/h;", IsCorrect = false, QuestionId = 20 },
					new Answer { Id = 62, Text = "90 km/h;", IsCorrect = false, QuestionId = 20 },
					new Answer { Id = 63, Text = "100 km/h.", IsCorrect = true, QuestionId = 20 },
					new Answer { Id = 64, Text = "pentru lipsa triunghiurilor reflectorizante;", IsCorrect = false, QuestionId = 21 },
					new Answer { Id = 65, Text = "pentru lipsa trusei medicale de prim ajutor;", IsCorrect = false, QuestionId = 21 },
					new Answer { Id = 66, Text = "pentru refuzul de legitimare;", IsCorrect = true, QuestionId = 21 },
					new Answer { Id = 67, Text = "folosirea mijloacelor de avertizare sonoră;", IsCorrect = false, QuestionId = 22 },
					new Answer { Id = 68, Text = "remorcarea altui autovehicul;", IsCorrect = false, QuestionId = 22 },
					new Answer { Id = 69, Text = "staţionarea voluntară pe partea carosabilă;", IsCorrect = true, QuestionId = 22 },
					new Answer { Id = 70, Text = "să verificaţi o dată pe lună, la rece, presiunea din pneuri;", IsCorrect = true, QuestionId = 23 },
					new Answer { Id = 71, Text = "să schimbaţi între ele pneurile, la aproximativ 10 000 km;	", IsCorrect = true, QuestionId = 23 },
					new Answer { Id = 72, Text = "să înlocuiţi pneurile la fiecare 1000 km.", IsCorrect = true, QuestionId = 23 },
					new Answer { Id = 73, Text = "semnalul luminos al semaforului instalat pe trotuar;", IsCorrect = false, QuestionId = 24 },
					new Answer { Id = 74, Text = "semnalul luminos al semaforului instalat pe consolă;", IsCorrect = true, QuestionId = 24 },
					new Answer { Id = 75, Text = "semnalul luminos al oricăruia dintre semafoare;", IsCorrect = false, QuestionId = 24 },
					new Answer { Id = 76, Text = "mărirea vitezei de deplasare sau grăbirea traversării drumului de către pietoni;	", IsCorrect = true, QuestionId = 25 },
					new Answer { Id = 77, Text = "oprirea temporară a circulaţiei;", IsCorrect = false, QuestionId = 25 },
					new Answer { Id = 78, Text = "reducerea vitezei de deplasare;", IsCorrect = false, QuestionId = 25 },
					new Answer { Id = 79, Text = "reduceţi viteza şi opriţi înaintea semibarierelor;	", IsCorrect = true, QuestionId = 26 },
					new Answer { Id = 80, Text = "ocoliţi semibariera şi vă continuaţi drumul, dacă trenul nu se apropie;", IsCorrect = false, QuestionId = 26 },
					new Answer { Id = 81, Text = "măriţi viteza pentru a nu fi surprinşi de închiderea semibarierei;	", IsCorrect = false, QuestionId = 26 },
					new Answer { Id = 82, Text = "cel mult 10 zile de la data constatării evenimentului;	", IsCorrect = false, QuestionId = 27 },
					new Answer { Id = 83, Text = "până la expirarea asigurării obligatorii de răspundere civilă auto;", IsCorrect = false, QuestionId = 27 },
					new Answer { Id = 84, Text = "cel mult 30 zile de la data producerii avariei;", IsCorrect = true, QuestionId = 27 }
			);
		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	 : base(options)
		{

		}

	}
}
