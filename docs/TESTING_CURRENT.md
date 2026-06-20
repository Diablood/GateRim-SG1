# Tests du jalon actif

Jalon : `0.3.17-dev - Refresh project and wiki presentation`

Branche attendue : `feature/project-presentation-refresh`

Base attendue : `v0.3.16-dev`

Révision locale validée : `0.3.17-dev-r2`

Version de DLL attendue : `0.3.17.0`

Statut : validation terminée sur `0.3.17-dev-r2` ; jalon clôturé et publié sous `v0.3.17-dev`.

## Résultat final validé

- Rebuild forcé validé avec la DLL `0.3.17.0`.
- Chargement jusqu'au menu principal validé sans nouvelle erreur GateRim SG-1.
- Version du mod `0.3.17-dev` et description About immersive validées.
- README, accueil du wiki et état du contenu relus sans ancien jalon présenté comme actif.
- Les liens testés du README et du wiki ouvrent les destinations prévues.
- La sidebar catégorisée reste lisible dans sa largeur réelle.
- Toutes les cibles internes de l'ancienne sidebar sont conservées sans doublon.
- `Tokra-Dual-Identity` et `Tokra-Tactical-Threat-Assessment` sont désormais présents.
- La distinction entre contenu jouable et contenu futur est cohérente.
- Aucun fichier de gameplay, Def, traduction, texture ou `About/ModIcon.png` n'est modifié.
- `Player.log` est propre pour le chargement testé.
- Décision finale : conserver la révision `r2` sans correctif supplémentaire.

## 1. Extraction et contrôle Git

```powershell
git branch --show-current
git status --short
git diff --check
```

La branche doit être `feature/project-presentation-refresh` et aucun fichier non prévu ne doit être modifié.

## 2. Rebuild et chargement minimal

Effectuer un rebuild forcé :

```powershell
dotnet build .\Source\GateRimSG1\GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

Vérifier :

- build réussi ;
- `1.6/Assemblies/GateRimSG1.dll` en version `0.3.17.0` ;
- chargement jusqu'au menu principal avec GateRim SG-1 et Biotech ;
- version du mod `0.3.17-dev` ;
- description About toujours lisible et immersive ;
- aucune nouvelle erreur rouge ni erreur GateRim SG-1 dans `Player.log`.

Aucun test de partie complète n'est requis : ce jalon ne modifie aucun code de gameplay, Def, traduction ou texture.

## 3. README du dépôt

Vérifier `README.md` dans l'aperçu GitHub ou Markdown :

- aucune présentation de `0.2.18-dev` comme jalon actif ;
- statut `0.3.17-dev`, RimWorld 1.6 et Biotech clairement indiqués ;
- contenu jouable résumé sans inventaire historique de micro-jalons ;
- Porte fonctionnelle explicitement indiquée comme non disponible ;
- liens vers le wiki, l'état du contenu, la roadmap, le changelog, le build et les tests ;
- aucun chemin local Windows ni instruction de publication destinée au joueur.

## 4. Accueil du wiki

Vérifier `docs/wiki/Home.md` :

- version documentée `0.3.17-dev` ;
- résumé cohérent avec `About/About.xml` ;
- mention des 70 backstories et de la double identité Tok'ra ;
- aucune future extension des backstories existantes présentée comme encore nécessaire ;
- futures races, storyteller, monde GateRim SG-1 et Porte fonctionnelle clairement séparés du contenu jouable ;
- tous les noms de pages liés correspondent à des pages existantes du wiki.

## 5. Sidebar du wiki

Vérifier `docs/wiki/_Sidebar.md` dans le wiki synchronisé ou dans un aperçu Markdown :

- la navigation n'est plus une liste linéaire unique ;
- les catégories Démarrer, Cultures et factions, Équipement et recherche, Symbiotes et implantation, Prim'ta et trétonine, Goa'uld, Tok'ra et Assistance sont clairement séparées ;
- tous les liens internes ouvrent une page existante ;
- aucun lien interne n'apparaît deux fois ;
- les anciennes pages restent accessibles, sans perte par rapport à la sidebar précédente ;
- `Tokra-Dual-Identity` et `Tokra-Tactical-Threat-Assessment` sont désormais présents ;
- le lien externe vers le dépôt principal fonctionne ;
- les intitulés restent assez courts pour une sidebar étroite.

## 6. État du contenu

Vérifier `docs/wiki/Content-Status.md` :

- dernière révision `0.3.17-dev` ;
- lignes ajoutées pour la refonte des backstories, les profils culturels de départ, la double identité Tok'ra, le diagnostic culturel et l'extension à 70 backstories ;
- suppression des lignes futures devenues fausses ou terminées, notamment l'enrichissement générique des histoires et l'extraction Tok'ra annoncée comme absente ;
- aucune fonctionnalité future présentée comme déjà jouable ;
- aucune fonctionnalité validée depuis `0.3.5-dev` laissée uniquement dans la section « Prévu ».

## 7. Contrôle des fichiers non concernés

```powershell
git status --short
git diff --name-only
```

La liste ne doit contenir que les onze fichiers annoncés dans `docs/PROJECT_STATE.md`.

Vérifier en particulier que les fichiers suivants ne sont pas modifiés :

- `About/ModIcon.png` ;
- `1.6/Defs/**` ;
- `1.6/Patches/**` ;
- `Languages/**` ;
- `Source/GateRimSG1/**/*.cs` ;
- `Textures/**`.

## Résultat à communiquer

- `Tests OK` si le rebuild, le menu principal, les trois pages de présentation et la sidebar catégorisée sont validés ;
- sinon, indiquer le fichier, le lien ou la formulation incorrecte observée.
