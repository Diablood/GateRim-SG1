# Symbiotes

> Statut : Prototype  
> Première fondation XML : 0.1.7-dev  
> Symbiote libre : 0.1.8-dev  
> Implantation récente : 0.1.11-dev  
> Implantation forcée interactive : 0.1.17-dev

## Vue d'ensemble

Les symbiotes sont une mécanique centrale de GateRim SG-1.

Plusieurs variantes partageront une base commune, mais leurs relations avec leurs hôtes resteront différentes.

| Type | Hôte | Relation prévue |
|---|---|---|
| Goa'uld adulte | Humain ou Unas | Prise de contrôle forcée |
| Tok'ra adulte | Hôte volontaire | Coexistence et partage du contrôle |
| Larve portée par un Jaffa | Jaffa | Soutien biologique sans possession |

## Cycle Goa'uld prévu

```text
Symbiote libre
    ↓ attaque sauvage ou rituel
Hôte récemment infesté
    ↓ période critique
Hôte Goa'uld actif
    ↓ extraction, mort ou transfert
Symbiote libre ou nouvel hôte
```

## État actuel

Depuis `0.1.7-dev`, le xenotype `hôte Goa'uld` permet de tester un pawn déjà possédé.

Depuis `0.1.8-dev`, le pawn `symbiote Goa'uld` permet de tester l'organisme libre en mode développeur.

Depuis `0.1.11-dev`, l'état de santé `implantation Goa'uld récente` représente la période critique après l'entrée dans un hôte.

Depuis `0.1.17-dev`, un symbiote libre peut appliquer cet état grâce à une commande manuelle lorsqu'il se trouve à côté d'un humanoïde adulte compatible. Son identité persistante est conservée lors du transfert.

## Éléments de gameplay prévus

- IA hostile autonome du symbiote libre ;
- interruption médicale ;
- conversion en hôte possédé lorsque le compte à rebours se termine ;
- transfert vers un nouvel hôte ;
- extraction spécialisée ;
- différenciation claire entre Goa'uld et Tok'ra ;
- dépendance future des Jaffa au symbiote ou à la trétonine.
