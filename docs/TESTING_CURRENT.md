# Tests courants

Jalon : `0.3.103-dev - Add Jaffa knife`

Révision validée : `r4`
Version de DLL validée : `0.3.103.0`

## Objet du test

Le jalon ajoute une première arme de mêlée Jaffa dédiée, la rend fabricable et
commercialisable, puis l'intègre aux sélections d'armes des troupes Jaffa afin
d'apporter de la diversité dans les bases, raids, caravanes et missions.

## Contrôles de clôture

Depuis la racine du dépôt :

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"

.\tools\check-duration-formatting.cmd
.\tools\test-documentation-consistency-guards.cmd
.\tools\check-documentation-consistency.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd `
  -ExpectedVersion 0.3.103-dev `
  -ExpectedBackstoryCount 83

.\tools\check-project-consistency.cmd -RequirePublicationReady

git diff --check
```

La baseline documentaire et visuelle finale est :

```text
Assembly: 0.3.103.0
Gameplay PNG files: 614
Local texture families: 81
Final local texture families: 55
Backstories: 83
```

## Validation RimWorld ciblée

La validation fonctionnelle du mainteneur confirme :

- apparition développeur et chargement sans texture manquante ;
- vraie transparence extérieure et absence de damier intégré ;
- texture finale retouchée avec un contour extérieur renforcé ;
- taille au sol et équipée validée avec `drawSize = 0.65` ;
- masse `0,85`, manche `9`, pointe `16`, tranchant `16` et récupération de
  `2` secondes conformes au profil retenu ;
- équipement, attaque de mêlée, fabrication et sauvegarde/rechargement valides ;
- présence occasionnelle dans le stock des convois Jaffa libres ;
- diversité d'armes visible chez les Jaffa des bases, raids et missions ;
- guerriers, gardes, officiers et marchands compatibles avec le couteau ;
- briseur de murs de la diversion Tok'ra toujours limité au Ma'Tok.

## Compatibilité confirmée

- `SG1_JaffaKnife` utilise un nouveau DefName et un nouveau chemin stable ;
- aucun DefName ou chemin d'arme existant n'est renommé ;
- les groupes et budgets de menace existants restent inchangés ;
- le couteau est une arme principale alternative et non une arme secondaire
  transportée en plus par un système de sidearm ;
- les parties existantes restent compatibles ;
- la synchronisation du wiki séparé est nécessaire pour publier la nouvelle page
  et l'image protégée.
