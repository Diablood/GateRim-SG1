# Traces biologiques persistantes de naquadah

> Statut : Implémenté
> Version : 0.3.58-dev

## Origine

Les traces de naquadah ne dépendent ni de la faction ni du rang social. Elles
apparaissent dans le sang d'un humain exposé durablement à un symbiote :

- hôte actif d'un symbiote adulte Goa'uld ou Tok'ra ;
- Jaffa portant un Prim'ta.

Le gène visible `naquadah dans le sang` sert de marqueur commun. Une fois acquis,
il reste présent après l'extraction du symbiote adulte ou le retrait du Prim'ta.
Les anciens hôtes conservent donc leur compatibilité biologique.

## Kara kesh

Un kara kesh peut être transporté, stocké ou porté par n'importe quel humain,
mais son bouclier ne s'active que si le porteur possède ces traces persistantes.
Sans elles :

- aucun tir n'est absorbé ;
- aucun champ ni indicateur d'énergie n'apparaît ;
- le porteur peut continuer à utiliser une arme à distance.

Dès que les traces sont présentes, le fonctionnement normal du bouclier reprend.

## Sauvegardes existantes

Au chargement, GateRim SG-1 vérifie les hôtes adultes, les Tok'ra, les Jaffa avec
Prim'ta, les caravanes, les dirigeants de faction et les pawns du monde. Les
personnages concernés reçoivent le marqueur manquant sans doublon.

## Limites actuelles

Cette première version ne simule ni quantité ni disparition progressive du
naquadah. Elle ne traite pas encore la manipulation du marqueur par les machines
génétiques de Biotech et n'ajoute aucune nouvelle fonction offensive au kara
kesh.
