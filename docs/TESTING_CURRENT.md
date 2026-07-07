# Current milestone test procedure

Jalon : `0.3.87-dev - Fix Jaffa and Tok'ra starter symbiotes`

Révision locale : `r3`

Version de DLL attendue : `0.3.87.0`

Statut : **révision finale `r3` validée et publiée**.

## Objectif

Valider que les états biologiques obligatoires sont créés pendant la génération
vanilla des personnages de départ, avant leur affichage sur la page de
configuration :

- un Jaffa adulte éligible porte déjà son Prim'ta ;
- tout pawn utilisant le xénotype `hôte Goa'uld` porte déjà un véritable
  symbiote adulte ;
- une carrière Tok'ra produit un symbiote d'origine Tok'ra avec deux identités ;
- une carrière Goa'uld produit un symbiote d'origine Goa'uld sans changement de
  personnalité Tok'ra.

Le correctif ne doit jamais réimplanter un symbiote après que le pawn a été
présenté au joueur. Une suppression volontaire réalisée avec un mod d'édition
sur cette page doit rester effective.

## Résultats observés avec r1 et r2

- le Jaffa reçoit correctement son Prim'ta ;
- lorsqu'un Tok'ra possède son symbiote, les deux identités et le changement de
  personnalité fonctionnent correctement ;
- certains rerolls du xénotype `hôte Goa'uld` restent sans symbiote ;
- la correction du garde transitoire dans `r2` ne change pas ce résultat.

La cause réelle est le profil culturel mixte : un même xénotype peut recevoir
une carrière Tok'ra ou Goa'uld, tandis que les deux premières révisions ne
créaient un symbiote que pour la branche Tok'ra. `r3` crée désormais un
symbiote pour les deux branches et utilise la carrière finale uniquement pour
déterminer l'origine persistante.

## Résultat final

La révision `r3` est validée par le mainteneur :

- le chemin Jaffa reste fonctionnel et systématique pour les adultes éligibles ;
- chaque starter `SG1_GoauldHost` reçoit exactement un symbiote adulte ;
- les carrières Tok'ra conservent la double identité et le changement de
  personnalité ;
- les carrières Goa'uld reçoivent l'origine Goa'uld sans commande Tok'ra ;
- les rerolls ne laissent plus de simple humain amélioré sans symbiote ;
- aucun doublon ou correctif de chargement n'est introduit.

## Préconditions

- partir de `develop` aligné avec `v0.3.86-dev` au commit
  `bbbb981fb54131964a05b6f7626b5472cbc399dd` ;
- travailler sur `fix/jaffa-tokra-starter-symbiotes` ;
- avoir déjà appliqué `0.3.87-dev-r1` puis `r2`, et appliquer le ZIP correctif
  `0.3.87-dev-r3` depuis la racine du dépôt ;
- activer Harmony et Biotech ;
- utiliser un scénario vanilla ou personnalisé normal, sans starter Jaffa ou
  Tok'ra imposé par GateRim ;
- garder le mode développeur désactivé pour le test principal.

## Build et contrôles automatiques

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd -ExpectedVersion 0.3.87-dev

git diff --check
git status --short
```

Résultats attendus :

- assembly `1.6/Assemblies/GateRimSG1.dll` en version `0.3.87.0` ;
- aucun échec XML, traduction, durée, asset ou cohérence ;
- aucune modification des comptes du registre visuel ;
- aucun ZIP ou script d'application indexé.

## Test principal : groupe mixte

Depuis la page vanilla où les pawns de départ peuvent être remplacés et
régénérés :

1. conserver deux humains ordinaires ;
2. choisir le xénotype `Jaffa` pour un troisième pawn ;
3. choisir le xénotype `hôte Goa'uld` pour un quatrième pawn ;
4. ouvrir le panneau Santé de chaque candidat avant de valider la partie.

Résultat attendu :

- les deux humains restent inchangés ;
- le Jaffa adulte affiche `symbiote du Prim'ta` ;
- l'hôte Goa'uld affiche `symbiote adulte de la famille Goa'uld` quelle que soit
  sa carrière finale ;
- une carrière Tok'ra affiche un nom d'hôte distinct, un nom de symbiote
  distinct, leurs parcours et la personnalité active ;
- une carrière Goa'uld affiche une origine Goa'uld et ne possède pas le gizmo de
  changement de personnalité Tok'ra ;
- aucun doublon de Hediff ou de trace de naquadah n'apparaît.

## Matrice de régénération r3

1. conserver le même emplacement de starter ;
2. sélectionner le xénotype `hôte Goa'uld` ;
3. effectuer au moins vingt rerolls ;
4. ouvrir immédiatement Santé pour chaque candidat ;
5. noter séparément la carrière finale et l'origine du symbiote.

Résultat attendu :

- `20/20` candidats possèdent exactement un symbiote adulte persistant avant
  validation ;
- chaque carrière Tok'ra produit une origine Tok'ra avec deux identités et un
  changement de personnalité fonctionnel ;
- chaque carrière Goa'uld produit une origine Goa'uld sans gizmo Tok'ra ;
- aucun candidat ne dépend de l'ordre des callbacks ou d'un ThingID réutilisé ;
- aucun doublon n'apparaît lorsque plusieurs notifications concernent la même
  génération.

## Régression Jaffa

1. sélectionner le xénotype `Jaffa` ;
2. effectuer au moins dix rerolls adultes ;
3. inspecter Santé avant chaque nouveau reroll.

Résultat attendu :

- chaque adulte éligible possède exactement un Prim'ta ;
- aucun pawn sous l'âge biologique minimal de `10` ans ne reçoit de Prim'ta
  forcé ;
- aucune dépendance au Prim'ta n'est déjà active sur un Jaffa correctement
  initialisé.

## Démarrage, identité et sauvegarde

1. valider un groupe contenant un Jaffa, un hôte Goa'uld d'origine Tok'ra et un
   hôte Goa'uld d'origine Goa'uld ;
2. confirmer que les pawns apparaissent normalement sur la carte ;
3. utiliser le gizmo de changement d'identité uniquement sur le Tok'ra ;
4. vérifier Bio, Social et Santé ;
5. sauvegarder puis recharger ;
6. basculer de nouveau l'identité Tok'ra.

Résultat attendu :

- le Jaffa conserve son Prim'ta ;
- les deux hôtes conservent le même identifiant et la même origine de symbiote ;
- le Tok'ra conserve ses deux identités et le changement de personnalité ;
- l'hôte Goa'uld ne reçoit jamais le gizmo Tok'ra ;
- les bonus de backstory ne s'empilent pas ;
- aucun composant biologique supplémentaire n'est créé au premier tick ou au
  chargement.

## Suppression volontaire avec un mod d'édition

Ce test nécessite uniquement un mod capable de retirer un Hediff depuis l'écran
de départ.

1. générer un Jaffa ou un hôte Goa'uld correctement initialisé ;
2. retirer volontairement son Prim'ta ou son symbiote adulte sans régénérer le
   pawn ;
3. valider la partie ;
4. attendre plusieurs jours, sauvegarder et recharger.

Résultat attendu :

- l'état retiré n'est pas recréé à la validation, au premier tick ni au
  chargement ;
- le Jaffa peut ensuite subir normalement la dépendance prévue par son absence
  de Prim'ta ;
- l'hôte privé de symbiote reste dans cet état tant que le joueur ne passe pas
  par une implantation réelle.

## Cas limites

- **Jaffa déjà porteur** : un état ajouté par une autre source avant le callback
  ne doit pas être dupliqué.
- **Hôte déjà porteur** : tout Hediff possédant
  `HediffComp_GoauldSymbiote` bloque une seconde création.
- **Jaffa trop jeune forcé par un mod** : aucun Prim'ta automatique avant l'âge
  biologique minimal de `10` ans.
- **Plusieurs Jaffa/hôtes** : chaque nouveau pawn est traité indépendamment.
- **Pawns non starters** : visiteurs, raids, quêtes, prisonniers et générations
  développeur conservent leurs initialisateurs historiques.
- **Scénario SG isolé** : les quatre Tau'ri prévus restent inchangés.

## Player.log

Vérifier l'absence de nouvelles erreurs relatives à :

```text
StarterSymbioteInitializer
ScenPart_CulturalStarterProfiles
SG1_Jaffa
SG1_GoauldHost
SG1_JaffaPrimta
SG1_GoauldHostSymbiote
GoauldSymbioteData
CulturalGeneratedHostIdentityUtility
DefOf
PawnGeneration
```

Avec les informations de debug avancées activées, une génération correcte peut
produire une trace d'initialisation par nouveau pawn. Aucune erreur ni warning ne
doit apparaître pendant les rerolls ordinaires.

## Critère de validation satisfait

La révision est validable seulement si :

- les états biologiques sont visibles avant la confirmation des starters ;
- chaque reroll `SG1_GoauldHost` produit exactement un symbiote adulte ;
- l'origine Tok'ra ou Goa'uld correspond à la carrière finale ;
- le démarrage et le rechargement ne dupliquent rien ;
- une suppression volontaire après génération reste respectée ;
- les générations non concernées ne changent pas ;
- le build, tous les contrôles et `Player.log` sont propres.
