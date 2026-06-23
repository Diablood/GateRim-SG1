# Current validation — 0.3.31-dev

Jalon : `0.3.31-dev - Add Tok'ra introduction artifact mission`
Version de DLL validée : `0.3.31.0`
Dernière révision locale validée : `r3`
Branche publiée : `feature/tokra-introduction-artifact-mission`
Tag final : `v0.3.31-dev`
Base : `v0.3.30-dev`

## Résultat final

Le jalon est validé localement et prêt pour son état publié définitif. Les correctifs consolidés de `r2` font partie de la base finale ; aucun suffixe `-rN` ne doit apparaître dans le commit ou le tag.

Couverture confirmée :

- état persistant de l'arc et première opportunité cachée `4–12` jours ;
- lettre à choix persistante avec acceptation et refus ;
- retour après refus, offre ignorée ou expiration dans `10–60` jours ;
- création du site uniquement après acceptation ;
- trajet et arrivée de caravane vanilla ;
- menace capturée à l'offre et garde introductive bornée à `2–6` défenseurs ;
- module physique exact suivi par son identité persistante ;
- combat, sélection du butin et reformation vanilla ;
- réussite uniquement lorsque le vrai module rejoint le joueur ;
- avertissement unique environ un jour avant l'échéance ;
- expiration réelle du site et destruction réelle du module ;
- nouveau délai `7–45` jours après une tentative acceptée échouée ;
- sauvegarde/rechargement pendant l'attente, l'offre, le site actif, la carte hostile, l'avertissement, l'échec et la réussite ;
- absence de second module, seconde offre, second résultat ou verrou prématuré ;
- fermeture permanente uniquement après la première récupération réussie ;
- séparation complète avec les six opérations récurrentes du communicateur.

## Passe éditoriale finale

Les lettres, actions, messages, statuts et descriptions visibles ont été relus. Leur formulation reste narrative et compréhensible ; les informations techniques demeurent dans les outils développeur et les documents internes. Aucun changement fonctionnel de texte n'a été nécessaire après la validation `r3`.

## Limites connues et suivi futur

- La texture du module reste provisoire jusqu'à la passe visuelle globale.
- Le profil `0,35`, `180–650` points et `2–6` défenseurs est fonctionnel et volontairement modéré. Un équilibrage prolongé sur davantage de richesses et de storytellers pourra encore ajuster ces valeurs.
- L'étude du module, la recherche Tok'ra dédiée, les prérequis du communicateur et le verrouillage des opérations récurrentes appartiennent aux jalons suivants.

## Contrôle final de publication

Depuis la racine du dépôt :

```powershell
./tools/check-project-consistency.cmd

./build.cmd `
    "D:/SteamLibrary/steamapps/common/RimWorld/RimWorldWin64_Data/Managed"
```

Après le rebuild final, vérifier une dernière fois l'absence d'erreur nouvelle dans `Player.log`, puis suivre `docs/MILESTONE_PUBLICATION.md` pour le commit, le tag et la synchronisation du wiki.
