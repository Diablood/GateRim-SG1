# Signal de planque Tok'ra

> Statut : Première base jouable  
> Première version : 0.2.13-dev

## Présentation

Une cellule Tok'ra clandestine peut désormais transmettre un bref signal chiffré
de planque.

Les coordonnées sont volontairement incomplètes : il ne s'agit pas encore d'un
site visitable. Le signal sert d'étape sûre avant de futurs refuges ou sites
cachés Tok'ra.

## Effet

Accuser réception du signal augmente légèrement la confiance Tok'ra et conserve une piste de planque :

```text
+1 confiance Tok'ra
+1 piste de planque Tok'ra
```

## Conditions

Le signal peut apparaître si la confiance Tok'ra est au moins neutre.

Il ne se déclenche pas lorsque les Tok'ra sont méfiants.

## Limites actuelles

Le signal ne crée pas :

- de site mondial ;
- de colonie ;
- de caravane ;
- de visiteurs ;
- de marchand ;
- de recrutement ;
- d'objet ;
- d'aide militaire ;
- de raid.

Les véritables planques visitables restent prévues pour un jalon ultérieur.
