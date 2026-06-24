# Outils de capture non létaux

La version `0.3.36-dev` prépare les futures missions demandant une cible vivante.

## Bolas

Les bolas sont une solution simple accessible tôt :

- fabrication au point d'artisanat ou dans une forge vanilla ;
- coût de `15` tissus et `8` aciers ;
- un seul lancer, consommé même en cas d'échec ;
- impact contondant léger de `3` dégâts ;
- neutralisation résistible ;
- effet de santé affiché comme une `entrave temporaire par bolas`, avec les jambes entravées.

## Fusil hypodermique expérimental Tok'ra

Le fusil est un dispositif Tok'ra spécialisé, pas une arme humaine classique :

- cinq charges scellées ;
- une charge consommée par tir, même si le projectile manque ;
- aucune recharge ni fabrication normale ;
- disparition après le cinquième tir ;
- impact contondant minime de `1` dégât ;
- neutralisation plus fiable que les bolas, mais jamais garantie ;
- charges restantes visibles dans le panneau d'inspection et dans les infobulles lorsque l'arme est équipée ou transportée.

Dans ce jalon, le fusil est fourni uniquement par les outils développeur. Une
future opération Tok'ra pourra l'attribuer comme ressource rare.

## Effets de neutralisation

Une fléchette Tok'ra réussie applique une inhibition neuromusculaire temporaire. Les bolas appliquent à la place une entrave physique autour des jambes. Les deux effets laissent la Conscience intacte mais retirent temporairement la capacité de Mouvement.

La cible tombe alors au sol par le système de santé ordinaire de RimWorld et
devient capturable avec l'ordre vanilla. Aucun ordre de maîtrise spécifique au
mod n'est nécessaire.

L'effet est appliqué séparément du faible impact physique. Lorsqu'il expire, une
cible qui ne souffre d'aucune autre incapacité se relève normalement.

Les faibles dégâts restent réels : une cible déjà très gravement blessée n'est
jamais totalement à l'abri.

## État du développement

Les deux outils et leur flux de capture vanilla sont validés. Cette étape
n'ajoute pas encore la mission récurrente de capture d'un officier Jaffa.

Les visuels sont provisoires et seront repris lors de la future passe graphique.
