# Implantation rituelle Goa'uld

> Statut : prototype jouable
> Première version : `0.1.22-dev`

## Présentation

![Icône finale de l'implantation rituelle](images/SG1_RitualImplantation.png)

Le symbiote Goa'uld libre dispose d'une voie d'implantation contrôlée, distincte
de sa chasse autonome.

## Utilisation

1. Générer ou récupérer un symbiote Goa'uld libre.
2. Désactiver sa chasse autonome pour préparer le rituel sans interruption.
3. Placer une cible humanoïde compatible et un bassin rituel à proximité.
4. Sélectionner le symbiote puis utiliser `Implantation rituelle`.
5. Choisir explicitement la cible dans le rayon autorisé.

Une cérémonie temporisée commence. Le symbiote, la cible et le bassin doivent
rester accessibles et suffisamment proches jusqu'à son terme.

## Conditions principales

```text
cible vivante et compatible
âge minimal de 13 ans
distance maximale de 12 cases pour le ciblage
bassin rituel contrôlé à moins de 6 cases
durée de cérémonie : 600 ticks
```

Le rituel peut être annulé manuellement. Il s'interrompt aussi si la cible
devient invalide, si un participant s'éloigne ou si le bassin est détruit.

La progression et la cible sont conservées dans la sauvegarde.

## Résultat

À la fin de la cérémonie, le symbiote libre disparaît et la cible reçoit une
implantation Goa'uld récente. L'identité persistante du parasite est conservée,
puis la conversion automatique en hôte actif suit le cycle habituel si aucune
extraction n'intervient.

## Limites actuelles

- aucune animation cérémonielle spécialisée ;
- pas de distinction particulière pour les prisonniers ;
- aucun système d'idéologie Goa'uld complet.

Le rituel de base fonctionne avec `Core + Biotech`. Une intégration optionnelle
à Ideology pourra ultérieurement ajouter rôles, participants et exigences de
lieu sans remplacer ce fonctionnement.

## Contrôle du symbiote

Le rite ne peut être commandé en jeu normal qu'avec un symbiote appartenant réellement au joueur. Un symbiote hostile simplement sélectionnable ne donne aucun droit de ciblage ou d'annulation. Le mode développeur conserve ces commandes pour les tests.
