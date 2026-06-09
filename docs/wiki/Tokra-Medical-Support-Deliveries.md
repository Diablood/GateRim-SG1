# Livraisons de soutien médical Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.53-dev

## Principe

À partir du palier de confiance coopérative, une équipe Tok'ra peut apporter
rarement de la trétonine même si aucun colon n'a besoin d'une symbiose.

Cette livraison est distincte de l'opportunité thérapeutique : elle ne fait pas
apparaître de symbiote libre et ne propose aucune implantation.

## Effets selon le palier

| Palier | Livraison | Visiteurs |
|---|---:|---:|
| Méfiante | indisponible | aucun |
| Neutre | indisponible | aucun |
| Coopérative | 2 doses physiques de trétonine | 1 hôte Tok'ra volontaire |
| Fiable | 4 doses physiques de trétonine + 1 médicament ultratechnologique | 2 hôtes Tok'ra volontaires |

Les ressources apparaissent près du point d'arrivée de l'équipe. Depuis
`0.1.56-dev`, une livraison fiable contient aussi `1` unité physique de
médicament ultratechnologique vanilla. Les visiteurs restent pacifiques et quittent naturellement la carte après une courte visite.

## Conditions naturelles

```text
première apparition possible : jour 45
chance XML de base           : 0,018
palier coopérative           : ×1,00 -> 0,018
palier fiable                : ×1,50 -> 0,027
délai minimal                : 60 jours
```

Le déclenchement manuel reste disponible depuis le menu développeur pour les
tests.

## Limites actuelles

Depuis `0.1.54-dev`, une confiance fiable augmente modérément leur fréquence
naturelle. La faction Tok'ra reste masquée. Les livraisons ne sont pas encore
des quêtes, ne reposent pas sur le commerce et ne modifient pas directement la
confiance.
