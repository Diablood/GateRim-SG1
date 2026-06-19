# Contact de planque Tok'ra

Une planque Tok'ra cachée peut contenir un unique contact pacifique.

Ce contact est volontairement limité :

- il n'est pas hostile ;
- il n'est pas marchand ;
- il n'est pas recrutable ;
- il ne fournit pas d'aide militaire ;
- il ne distribue pas encore de quête.

Depuis `0.2.20-dev`, un colon peut effectuer un court échange avec lui.
Sélectionnez le colon, effectuez un clic droit sur le contact Tok'ra puis
choisissez l'option d'échange. Le dialogue apporte une reconnaissance narrative,
une légère amélioration qualitative de la confiance Tok'ra et de l'expérience
en Médecine au colon choisi.

Cette interaction ne peut être utilisée qu'une seule fois pour chaque contact
de planque généré.

Depuis `0.2.21-dev`, l'échange prend la forme d'un bref conseil médical de
terrain. Depuis `0.2.22-dev`, son contenu et l'expérience accordée varient
légèrement selon la confiance actuelle. Depuis `0.2.23-dev`, le dialogue inclut
aussi un indice médical adapté au palier :

| Palier de confiance Tok'ra | XP en Médecine |
|---|---:|
| Méfiante | 250 |
| Neutre | 400 |
| Coopérative | 600 |
| Fiable | 800 |

L'indice supplémentaire reste narratif. Il n'ouvre ni commerce, ni recrutement,
ni aide militaire, ni traitement direct, ni récompense matérielle, ni chaîne
de quête.

Depuis `0.2.23-dev-r1`, le briefing détaillé s'ouvre dans une fenêtre vanilla,
tandis que l'historique des messages conserve un résumé court avec l'expérience
en Médecine reçue.

Depuis `0.2.24-dev`, un contact coopératif ou fiable peut également transmettre
une piste de planque supplémentaire si le registre dispose encore d'une place.
Cette piste alimente les systèmes de suivi existants sans créer immédiatement
de quête, caravane, marchand, objet ou traitement direct. Les contacts méfiants
et neutres restent limités au briefing médical.

## Note de test développeur

L'action développeur de préparation d'une planque crée l'environnement de test
sans modifier la confiance Tok'ra actuelle. Les actions dédiées permettent
d'ajuster la confiance avant de générer une nouvelle planque afin de vérifier
les différents paliers.
