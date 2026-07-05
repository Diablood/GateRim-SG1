# Current milestone validation

Jalon : `0.3.74-dev - Add distinctive Jaffa capture-officer appearance`

Branche finale :
`feature/distinctive-jaffa-capture-officer-appearance`

Révision locale finale : `r2`

Version de DLL validée : `0.3.74.0`

Statut : validation terminée et jalon publié sous `v0.3.74-dev`.

## Résultat final

- `git diff --check` : réussi ;
- build forcé `0.3.74.0` : réussi ;
- audit des durées : `104` clés uniques, réussi ;
- contrôle global de cohérence : réussi ;
- recherche `Armures Jaffa` et factures des deux pièces visibles : validées ;
- état rétracté sans facture indépendante : validé ;
- textures rouges, protections et `Impact social +10 %` : validés ;
- cible générée avec arme, armure rouge, casque rouge, gantelets et bottes :
  validée après la correction `r2` ;
- escortes conservant les équipements marron/doré : validées ;
- absence des variantes d'officier sur les Jaffa ordinaires : validée ;
- modes automatique, toujours déployé et toujours rétracté : validés ;
- séparation complète entre la paire standard et la paire d'officier : validée ;
- sauvegarde/rechargement de l'équipement et du mode : validé ;
- capture, transport en caravane et extraction Tok'ra : validés ;
- `Player.log` : aucune nouvelle erreur C#, Harmony, XML, DefOf, texture,
  apparel, rendu, Scribe, caravane ou mission.

## Historique de la révision

La révision `r1` avait validé la recherche, la fabrication, les textures,
l'équipement manuel, les protections et le bonus social. Elle avait également
révélé que la cible générée conservait son arme, ses gantelets et ses bottes,
mais ne recevait ni l'armure ni le casque dédiés.

La révision cumulative `r2` conserve les deux variantes à commonalité nulle et
ajoute une garantie ciblée après génération. L'armure et le casque sont créés et
équipés uniquement sur la cible de capture lorsqu'ils manquent. La correction
est validée sans modification du reste du flux de mission.

## Couverture durable à conserver

- les Defs et chemins de texture dédiés restent stables malgré le remplacement
  futur des PNG temporaires ;
- l'armure applique seule `SocialImpact +0.10` ;
- le casque ne change qu'entre ses deux Defs d'officier ;
- les pièces restent exclues de la génération aléatoire ;
- la fabrication reste liée à `SG1_JaffaArmor` ;
- les guerriers, gardes, escortes et colonies utilisent encore les équipements
  standards ;
- l'extension des officiers aux groupes Goa'uld ordinaires reste un futur jalon
  séparé.
