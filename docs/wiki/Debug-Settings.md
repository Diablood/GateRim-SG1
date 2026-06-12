# Réglages de debug avancés

Depuis `0.1.75-dev`, **GateRim SG-1** ajoute une option dédiée dans les
paramètres des mods :

```text
Afficher les informations de debug avancées de GateRim SG-1
```

## Comportement par défaut

L'option est désactivée par défaut. Les panneaux d'inspection normaux restent
plus lisibles :

- les identifiants persistants bruts des symbiotes libres sont masqués ;
- les délais bruts de chasse autonome en ticks sont masqués ;
- le délai brut d'extraction depuis une reine Goa'uld est masqué ;
- une offre thérapeutique Tok'ra affiche son palier de confiance sans exposer
  la valeur numérique interne.

Les informations contextuelles utiles restent visibles pendant les offres et
les rituels actifs.

## Lorsque l'option est activée

Les informations techniques supplémentaires réapparaissent dans les panneaux
d'inspection. Les traces détaillées `GR_Log.Message(...)` sont également
écrites directement dans `Player.log`.

Ces traces informatives n'entrent pas dans la fenêtre de journal interne de
RimWorld et ne doivent donc pas ouvrir de popup ressemblant à une erreur. Cela
inclut les refus normaux d'un incident Tok'ra forcé pour un test, par exemple
l'absence de colon malade admissible ou un palier de confiance insuffisant.

Le mode développeur de RimWorld active automatiquement les diagnostics
avancés, même si l'option du mod est désactivée.

## Avertissements et erreurs

Les avertissements et erreurs restent toujours écrits dans `Player.log`.
L'option ne masque jamais les anomalies utiles au dépannage.
