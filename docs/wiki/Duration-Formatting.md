# Formatage des durées

> Statut : deux passes fonctionnelles validées ; audit statique final corrigé en `0.3.71-dev-r5`

GateRim SG-1 utilise progressivement le format temporel localisé de RimWorld
pour les délais visibles par le joueur. Selon la durée réelle, le jeu peut
afficher des heures, des jours, des quadrums ou des années au lieu de convertir
toutes les échéances en un grand nombre d'heures.

La première passe validée couvre les principaux sites mondiaux, l'extraction
d'un officier Jaffa, le site mondial de bataille Goa'uld et le compte à rebours
des renforts du relais.

La deuxième passe validée étend ce format aux lettres d’offre et aux statuts
des huit familles d’opérations Tok’ra, aux refroidissements et dialogues du
communicateur sécurisé, aux étapes en attente de la mission Tok’ra unique, à
l’estimation d’une menace interceptée et à l’ancien marqueur mondial de planque.

La troisième révision ajoute un audit automatique du dépôt. Il détecte les
traductions qui accoleraient encore une unité fixe à une durée dynamique et les
convertisseurs manuels proches de l’interface joueur. Sa première passe a déjà
repéré et corrigé les suffixes résiduels des inspections et commandes du relais
Goa’uld décodé. Ce contrôle empêchera les futures fonctionnalités de
réintroduire silencieusement de grands totaux en heures ou des suffixes doublés.

Le changement reste uniquement visuel : les échéances, arrivées, délais,
refroidissements, probabilités et conditions de réussite ne sont pas modifiés.
Les diagnostics développeur peuvent encore afficher des ticks bruts.


## Contrôle de cohérence

La révision cumulative `r5` ajoute les dernières clés détectées par le premier
audit exécutable : variantes d’offre du framework, offre thérapeutique,
récupération de la reine Goa’uld, cooldown diplomatique et fallback historique
du site de bataille. Le détecteur C# distingue désormais les véritables
conversions temporelles des appels vanilla, des calculs mécaniques et des
actions développeur.
