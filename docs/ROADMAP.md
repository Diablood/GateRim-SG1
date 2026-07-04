# Roadmap

Ce fichier est le backlog durable du projet. Il conserve les travaux ouverts,
les règles qui doivent guider de futurs jalons et le dernier jalon en cours.
L'historique publié appartient à `docs/CHANGELOG.md` et aux tags Git ; les pistes
non décidées appartiennent à `docs/IDEAS_TO_REVISIT.md`.

## Jalon validé et publié - Profils persistants de doctrine des domaines Goa'uld (`0.3.64-dev`)

- [x] Partir du véritable dernier tag publié `v0.3.63-dev` sur
  `feature/goauld-domain-doctrine-profiles`.
- [x] Définir trois profils pilotés par Defs : conquête `4/1/1`,
  asservissement `2/3/1` et terre brûlée `2/1/3`.
- [x] Attacher le profil à l'instance de faction plutôt qu'au dirigeant actuel.
- [x] Réconcilier les anciennes sauvegardes et les mondes multi-domaines.
- [x] Conserver l'incident naturel unique, ses points vanilla, sa fréquence,
  son délai et ses seuils contextuels.
- [x] Modifier uniquement les poids relatifs des trois doctrines existantes.
- [x] Ajouter l'affichage qualitatif, les diagnostics et les commandes ciblées.
- [x] Corriger la traduction française via des clés explicites.
- [x] Ajouter une politique `.gitattributes` stable.
- [x] Réintégrer le jalon dans la structure documentaire consolidée de
  `0.3.63-dev`, sans restaurer les documents supprimés.
- [x] Retirer de la sidebar le lien obsolète vers la roadmap Tok'ra supprimée
  pendant la consolidation documentaire.
- [x] Exécuter le rebuild forcé `0.3.64.0` et les contrôles de cohérence.
- [x] Valider le test ciblé, la sauvegarde/recharge et `Player.log`.
- [x] Publier branche, tag annoté `v0.3.64-dev` et wiki après validation et
  autorisation explicite.

La révision finale `r2` est publiée avec la branche dédiée, le tag annoté
`v0.3.64-dev` et le wiki séparé synchronisé. `r1` avait réintroduit un lien wiki
supprimé par la consolidation ; `r2` corrige uniquement cette incohérence
documentaire sans modifier le gameplay déjà validé.


## Registre d'idées non planifiées

Les pistes exploratoires sans jalon décidé sont conservées dans
[`docs/IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md).

## Présentation et publication publique

- [ ] Maintenir la description Workshop à partir du README, de
  `About/About.xml` et de l'accueil du wiki.

## Passe visuelle globale

- [ ] Auditer les visuels provisoires ou trompeurs.
- [ ] Remplacer le dispositif d'observation portable.
- [ ] Vérifier le module de renseignements Tok'ra et les objets d'opération.
- [ ] Diversifier les icônes de sites et missions mondiales.
- [ ] Donner à l'officier Jaffa capturable une apparence distinctive.
- [ ] Harmoniser les identités visuelles Tok'ra, Goa'uld, Jaffa et SGC.
- [ ] Ajouter les visuels définitifs au wiki et à la présentation Workshop.

## Équipement Goa'uld et attributs de rang

- [ ] Étudier chaque dispositif comme un objet ou système distinct.
- [ ] Tester armes, mêlée, IEM, caravanes et sauvegarde.
- [ ] Refléter la puissance dans `combatPower`, menace, valeur et acquisition.
- [ ] Réserver les technologies fortes aux rangs cohérents.

## Pression et rivalités des domaines Goa'uld

- [ ] Étendre exigences et ultimatums uniquement depuis une cause visible.
- [ ] Représenter les conflits d'abord par des informations RP.
- [ ] Concevoir des garde-fous avant expansion ou destruction de colonies.

## Reines Goa'uld

Les extensions spéculatives de l'origine des larves restent dans
`docs/IDEAS_TO_REVISIT.md`.

## Opérations Tok'ra organiques

- [ ] Continuer à tester les huit archétypes publiés.
- [ ] Corriger uniquement les défauts observés en partie.
- [ ] Conserver un seul slot visible, les délais cachés et l'anti-répétition.
- [ ] Réserver les diagnostics complets aux outils avancés.
- [ ] Maintenir des variantes RP solides.

## Missions et questlines

- [ ] Garder les missions récurrentes rééligibles lorsqu'elles le prévoient.
- [ ] Dimensionner les menaces depuis la difficulté et la valeur de colonie.
- [ ] Étendre le framework commun uniquement sur plusieurs besoins réels.
- [ ] Conserver des adaptateurs spécialisés pour les cas atypiques.

## Interface et outils de debug

- [ ] Regrouper les actions par appareil et par phase.
- [ ] Garder les détails techniques dans les rapports et les logs.
- [ ] Vérifier qu'aucune action n'est visible hors mode développeur.
- [ ] Effectuer une passe finale sur les libellés et le ton RP.

## Équipement Tau'ri / SGC

- [ ] Ajouter les futures variantes de pantalons et vestes par XML pondéré.
- [ ] Conserver les armes humaines vanilla, avec substitutions facultatives.

## Framework culturel, noms et backstories

- [ ] Réutiliser les profils culturels lorsqu'ils expriment un besoin réel.
- [ ] Ajouter Asgard, Nox, Unas et autres cultures lors de leur création.
- [ ] Maintenir les tableaux wiki et les contrôles de couverture.

## Origines d'hôtes et identité Tok'ra

- [ ] Ajouter des origines pondérées seulement lorsque les cultures existent.
- [ ] Garder le moteur générique et privilégier les ajouts XML.
- [ ] Ne jamais réécrire l'origine d'une implantation réelle.
- [ ] Tester les mods tiers uniquement sur incompatibilité concrète.

## Futures races et factions

- [ ] Asgard : commerce, assistance et missions sans colonie obligatoire.
- [ ] Nox : présence pacifique, diplomatique et commerciale.
- [ ] Unas : variantes sauvages ou tribales et compatibilité comme hôtes.

## Monde entièrement GateRim SG-1

- [ ] Ajouter un préréglage optionnel sans factions vanilla sélectionnables.
- [ ] Couvrir les rôles économiques, raids, commerce, relations et victoire.
- [ ] Garder ce mode facultatif et le contenu compatible avec les parties normales.

## Storyteller et orchestration

- [ ] Concevoir un storyteller GateRim SG-1 optionnel.
- [ ] Orchestrer les contenus sans les rendre dépendants de ce storyteller.
- [ ] Espacer les événements pour éviter les successions artificielles.

## Progression Stargate

- [ ] Continuer les fondations et expéditions.
- [ ] Introduire la Porte seulement lorsque le jeu sans Porte est solide.
- [ ] Préserver le contenu actuel comme autonome.

## Documentation et wiki

- [ ] Maintenir d'abord le wiki français.
- [ ] Mettre à jour l'accueil, l'état du contenu et les pages de sous-système.
- [ ] Conserver l'anglais pour les identifiants techniques lorsque nécessaire.
- [ ] Mettre à jour un document existant avant d'en créer un nouveau.

## Audits transversaux

- [ ] Réexaminer stockage, nourriture, recettes et commerce.
- [ ] Conserver les compatibilités DLC exploratoires dans les idées à revoir.

## Maintenance du projet

- [ ] Continuer sur des branches `feature/...` depuis le dernier tag.
- [ ] Maintenir état, roadmap, tests et changelog selon `docs/README.md`.
- [ ] Signaler les suppressions avant extraction d'un ZIP.
- [ ] Garder les ZIP ignorés à la racine.
- [ ] Préserver `About/ModIcon.png`.

## Règle de clôture

Lorsqu'un élément est terminé :

1. décrire le résultat dans le changelog et l'état courant ;
2. retirer sa checklist de cette roadmap, sauf règle durable ;
3. inscrire les travaux décidés dans le backlog ;
4. transférer les pistes exploratoires dans `IDEAS_TO_REVISIT.md` ;
5. utiliser Git et les tags comme archive.
