# Sprendimo aprašymas

Užduotį sprendžiau Visual Studio aplinkoje, naudojantis .NET Core Console aplikacija. 

Pradžioje vos pamačius užduotį buvo šovusi idėja, žodžius sudarinėti besukant ciklą, bet pasirinkau variantą, kad žodžius iškart galima skaidyti į 
sąrašą ir atsižvelgiant į maksimalų eilutės ilgį yra dėliojamas į teksto dalių sąrašą. 

Žodžiai yra dedami likusioje eilutės dalyje, tik tuo atveju, jeigu pusės žodžio 
ilgis yra lygus arba didesnis už likusią laisvą vietą eilutėje, kitu atveju žodis yra įrašomas naujoje eilutėje. 

Bededant žodžius į teksto dalių sąrašo eilutes  yra, taip pat, tikrinama, ar žodis yra didesnis už galimą maksimalų eilutės ilgį teksto dalių sąraše, jeigu jis ilgesnis, tokiu 
atveju skaidomas tol, kol žodis tampa 0-linio ilgio. Žodžių apdorojimas yra skirstomas į dvi dalis, tai pirmo žodžio ir likusiųjų, kadangi pirmą 
žodį dedant skiriasi, tai kad nėra dar nieko įdėta teksto dalių sąrašą, dėl to nereikia tikrinti likusios eilutės ilgio, lyg jau būtų įrašytų duomenų.

Pradiniai duomenys skaitomi iš failo. Galutiniai rezultatai išvedami į console langą ir atspausdinami į tekstinį failą.

# Užduotis
 
Įgyvendinkite algoritmą, kuris priima 2 argumentus: tekstą ir raidžių kiekį eilutėje. Algoritmas turi grąžinti tekstą eilutėmis taip, kad nė 
viena eilutė nebūtų ilgesnė nei nurodytas raidžių kiekis eilutėje. Stenkitės dalinti eilutes žodžio ribose.
Sprendimą pateikite kartu su automatizuotais testais.

Laukiame programinio kodo c#.net kalba.

Pageidautina, kad pradinis tekstas būtų nuskaitytas iš failo, o rezultatas – įrašytas į failą.
 
# PAVYZDYS
Tekstas: "žodis žodis žodis"

Raidžių kiekis eilutėje: 13

Rezultatas (2 eilutės):

žodis žodis

žodis
 
# PAVYZDYS

Tekstas: "šiuolaikiškas ir mano žodis"

Raidžių kiekis eilutėje: 7

Rezultatas (4 eilutės):

šiuolai

kiškas

ir mano

žodis
