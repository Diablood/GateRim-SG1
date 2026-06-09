# Pondérations storyteller Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.54-dev

## Principe

La confiance Tok'ra modifie désormais la probabilité naturelle de sélection de
deux incidents existants. Les valeurs XML restent des chances de base lisibles,
puis les workers appliquent un multiplicateur selon le palier courant.

Le déclenchement manuel depuis le menu développeur reste disponible pour les
tests.

## Opportunités thérapeutiques

| Palier | Multiplicateur | Chance de base effective |
|---|---:|---:|
| Méfiante | ×0,50 | 0,0175 |
| Neutre | ×1,00 | 0,0350 |
| Coopérative | ×1,25 | 0,04375 |
| Fiable | ×1,50 | 0,0525 |

Les conditions médicales, la première apparition possible au jour `30` et le
délai minimal de `60` jours restent inchangés.

## Livraisons médicales indépendantes

| Palier | Multiplicateur | Chance de base effective |
|---|---:|---:|
| Méfiante | ×0,00 | indisponible |
| Neutre | ×0,00 | indisponible |
| Coopérative | ×1,00 | 0,018 |
| Fiable | ×1,50 | 0,027 |

La première livraison naturelle reste possible à partir du jour `45`, avec un
délai minimal de `60` jours.

## Limites actuelles

La fréquence des visiteurs Tok'ra pacifiques ordinaires reste inchangée. Les
quêtes, la diplomatie vanilla et les colonies Tok'ra viendront dans des jalons
séparés.
