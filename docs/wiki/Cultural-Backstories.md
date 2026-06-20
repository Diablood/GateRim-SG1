# Histoires culturelles

> Statut : Base culturelle enrichie
>
> Version documentée : `0.3.9-dev`

GateRim SG-1 utilise des histoires culturelles natives de RimWorld sans dépendre de Humanoid Alien Races.

Les `52` histoires existantes ont été conservées, mais leurs descriptions sont désormais plus détaillées en anglais comme en français. Chacune explique à la fois l'origine du personnage et les habitudes pratiques acquises au cours de sa vie.

## Effets en jeu

Chaque histoire culturelle apporte désormais de petits bonus de compétences cohérents avec son parcours.

- Les enfances donnent des bases modestes dans quelques domaines.
- Les carrières adultes proposent une spécialisation plus visible.
- Aucune histoire n'impose de trait, de passion ou d'incapacité de travail.
- Les bonus complètent la génération du personnage sans décider seuls de tout son profil.


## Randomisation des pawns de départ

Le bouton de randomisation vanilla applique désormais des profils culturels uniquement aux pawns de départ concernés :

- un starter **Jaffa** reçoit une enfance Jaffa et une carrière adulte Jaffa Goa'uld ou Jaffa libre ;
- un starter **hôte Goa'uld** reçoit une enfance humaine hors-monde et une carrière adulte Goa'uld ou Tok'ra ;
- dans un scénario normal, un starter humain conserve l'accès aux enfances et carrières vanilla, tandis que les six parcours Tau'ri / SGC rejoignent le même ensemble pondéré de carrières adultes compatibles ;
- dans le scénario **Équipe SG isolée**, les carrières adultes sont limitées aux six parcours SGC actuels. Les enfances restent vanilla tant qu'un ensemble Tau'ri dédié n'a pas été conçu.

Les pawns générés par les raids, visiteurs, colonies, incidents et quêtes conservent leurs filtres habituels. Une sélection manuelle réalisée après la génération par un éditeur de pawns compatible reste libre.

<!-- BACKSTORY_TABLES_START -->
## Catalogue détaillé

Les tableaux suivants reprennent toutes les backstories actuellement intégrées au mod. Les valeurs indiquées dans la colonne **Compétences** sont les bonus directement accordés par la backstory ; elles s'ajoutent aux autres éléments de génération du personnage.

À chaque ajout ou modification future d'une backstory, le tableau de la culture concernée doit être mis à jour dans le même jalon.

### Tau'ri / SGC

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Âge adulte | soldat des forces spéciales du SGC | Affecté au programme Porte des étoiles après une carrière militaire exigeante, ce soldat a appris à improviser sous le feu et protéger son équipe loin de la Terre. Les déploiements répétés lui ont appris à comprendre rapidement un terrain inconnu et à maintenir les spécialistes moins expérimentés en mouvement. | Tir +4, Mêlée +2, Social +1 |
| Âge adulte | médecin de terrain du SGC | Ce médecin a rejoint les équipes hors-monde afin de maintenir les explorateurs en vie lorsque l'évacuation par la Porte des étoiles ne pouvait pas être garantie. Le triage de terrain, les cliniques improvisées et les agents pathogènes extraterrestres ont rendu le sang-froid aussi important que la technique médicale. | Médecine +5, Intellectuel +2, Social +1 |
| Âge adulte | scientifique du SGC | Recruté pour étudier les technologies extraterrestres et les biologies inconnues, ce scientifique s'est habitué à prendre des décisions prudentes avec des informations incomplètes. Des années de laboratoires sécurisés et d'expéditions lui ont appris à transformer des découvertes impossibles en réponses pratiques. | Intellectuel +5, Artisanat +2 |
| Âge adulte | linguiste du SGC | Ce spécialiste a étudié les langues, les mythes et les inscriptions anciennes avant de rejoindre des missions où une mauvaise traduction pouvait mettre en danger toute une équipe. Il a appris à obtenir la coopération par une observation patiente, des questions respectueuses et une mémoire précise des détails culturels. | Social +4, Intellectuel +3 |
| Âge adulte | ingénieur de terrain du SGC | Formé pour maintenir le matériel en état dans des conditions difficiles, cet ingénieur accompagnait les équipes SG lorsque les missions lointaines exigeaient des solutions pratiques. Lorsque les pièces standard manquaient, il a appris à reconstruire des appareils avec de la récupération et à expliquer les réparations au reste de l'équipe. | Construction +4, Artisanat +4, Intellectuel +1 |
| Âge adulte | officier de liaison du SGC | Cet officier a appris à concilier les priorités militaires, la prudence scientifique et les besoins des communautés rencontrées au-delà de la Porte des étoiles. Sa réussite dépendait de sa capacité à lire une assemblée, traduire des objectifs opposés et donner aux alliés une raison de poursuivre le dialogue. | Social +5, Intellectuel +2 |

### Jaffa — enfances communes

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Enfance | enfant d'un village Jaffa | Élevé dans un village Jaffa sous des étoiles lointaines, cet enfant a appris les devoirs communautaires, les anciens récits et la discipline du foyer. Le travail quotidien dans les champs et les cuisines lui a donné des habitudes pratiques et un profond sens du devoir envers ses voisins. | Plantes +2, Cuisine +1, Social +1 |
| Enfance | jeune serviteur de temple | Cet enfant a servi autour d'un temple Goa'uld, portant des messages et observant les cérémonies bien avant d'en comprendre les pouvoirs. Les couloirs silencieux lui ont appris à reconnaître le rang, le rituel et le danger dans les gestes les plus discrets. | Social +2, Intellectuel +1, Art +1 |
| Enfance | enfant d'une famille de guerriers | Né dans une famille de combattants Jaffa, cet enfant a grandi parmi les exercices, l'entretien des armures et l'attente du service. Avant même son entraînement officiel, il savait démonter des armes simples et reconnaître le rythme d'une patrouille. | Mêlée +2, Tir +1, Artisanat +1 |
| Enfance | jeune berger d'un monde soumis | Cet enfant gardait les troupeaux d'un monde tributaire et a appris à survivre loin des colonies fortifiées. De longues journées au-delà des murs ont développé son aisance avec les animaux et sa connaissance des plantes utiles. | Animaux +3, Plantes +1 |
| Enfance | apprenti artisan Jaffa | Placé auprès d'artisans locaux, cet enfant a appris les gestes patients qui permettaient à une communauté Jaffa de rester équipée et nourrie. Les petites réparations et les découpes mesurées récompensaient la patience, la précision et le respect des matériaux rares. | Artisanat +3, Construction +1 |
| Enfance | enfant d'une forteresse Jaffa | Cet enfant a grandi dans une garnison Jaffa fortifiée, où les alarmes, les patrouilles et les bandes de guerre faisaient partie du quotidien. Il a appris où se mettre à couvert, comment transporter les fournitures et quand une alarme annonçait un danger réel. | Tir +2, Mêlée +2 |
| Enfance | pupille d'un sanctuaire Jaffa | Recueilli par une communauté sanctuaire, cet enfant a appris la prudence, l'entraide et la valeur de l'écoute avant la parole. Aider les blessés et rassurer les nouveaux arrivants apeurés a fait de la compassion une compétence pratique de survie. | Médecine +2, Social +2 |
| Enfance | jeune d'un camp d'entraînement Jaffa | Cet enfant a passé ses années de formation autour d'un camp d'entraînement Jaffa, aidant aux tâches simples pendant que les vétérans préparaient la génération suivante. Les exercices, les corvées et les combats observés lui ont donné une familiarité précoce avec les armes et la discipline. | Mêlée +2, Tir +2 |

### Jaffa au service des Goa'uld

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Âge adulte | guerrier Jaffa de domaine | Ce Jaffa a combattu pour un domaine Goa'uld, défendant ses frontières et obéissant aux ordres transmis au nom d'un maître lointain. La vie de campagne lui a donné une solide maîtrise du bâton et de la lame, mais aussi l'habitude des ordres soutenus par la peur. | Tir +4, Mêlée +3 |
| Âge adulte | garde de temple Goa'uld | Affecté à un temple Goa'uld, ce Jaffa a protégé les lieux sacrés et appris les rites d'une hiérarchie rigide. Le devoir cérémoniel mêlait combat rapproché, vigilance et capacité à contenir les fidèles sans provoquer de désordre ouvert. | Mêlée +4, Tir +2, Social +1 |
| Âge adulte | garde de forteresse Goa'uld | Ce Jaffa a servi durant des années dans une forteresse, entretenant les armes, entraînant les patrouilles et réagissant rapidement aux alarmes. Il savait tenir une ligne de tir, renforcer une position endommagée et maintenir le matériel en état pendant un siège. | Tir +4, Construction +2, Artisanat +1 |
| Âge adulte | vétéran des guerres entre Grands Maîtres | Ce Jaffa a survécu aux campagnes entre Grands Maîtres rivaux et appris que la discipline au combat comptait davantage que les promesses. Les cicatrices et les camarades perdus lui ont appris à tirer avec constance, combattre au corps à corps et maintenir les blessés en vie. | Tir +4, Mêlée +3, Médecine +1 |
| Âge adulte | instructeur Jaffa | Chargé de préparer les jeunes guerriers, ce Jaffa a répété les anciennes leçons jusqu'à ce que les recrues puissent agir sans hésiter. Il a appris à démontrer chaque mouvement clairement, corriger les hésitations et imposer le respect aux recrues réticentes. | Mêlée +3, Tir +3, Social +2 |
| Âge adulte | escorte de procession Goa'uld | Ce Jaffa a escorté dignitaires, prêtres et caravanes de tribut, restant vigilant lorsque les démonstrations de pouvoir attiraient le ressentiment. Les routes encombrées exigeaient des tirs précis, une force maîtrisée et une attention constante à l'humeur du cortège. | Tir +3, Mêlée +2, Social +2 |
| Âge adulte | officier de garnison Jaffa | Promu dans une garnison de domaine, ce Jaffa a coordonné les sentinelles, les fournitures et la discipline. Le commandement exigeait plus que de la force : il devait équilibrer la logistique, l'obéissance et les ambitions des guerriers subordonnés. | Tir +3, Social +3, Intellectuel +1 |
| Âge adulte | sentinelle de Chappa'ai | Ce Jaffa a gardé un Chappa'ai et considéré chaque activation comme l'arrivée possible d'alliés, de pillards ou de supérieurs mécontents. Des années auprès de la porte ont affûté ses réflexes et sa connaissance des signes annonçant une arrivée dangereuse. | Tir +4, Mêlée +2, Intellectuel +1 |

### Jaffa libres

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Âge adulte | guerrier Jaffa affranchi | Ce Jaffa a rompu avec le service des Goa'uld et appris à combattre pour une communauté plutôt que pour l'orgueil d'un Grand Maître. La liberté n'a pas effacé ses habitudes militaires, mais lui a donné une raison de protéger ses compagnons plutôt que d'impressionner un dieu. | Tir +3, Mêlée +3, Social +1 |
| Âge adulte | protecteur d'une communauté Jaffa libre | Ce Jaffa a défendu une colonie indépendante où chaque patrouille protégeait les familles, les champs et la fragile idée de liberté. Il a appris à défendre les foyers sans négliger les récoltes, les conflits locaux ni la confiance des habitants. | Tir +3, Mêlée +2, Plantes +1, Social +1 |
| Âge adulte | vétéran de la rébellion Jaffa | Ce Jaffa a combattu lors d'un soulèvement contre la domination Goa'uld et conservé les leçons tirées des victoires et des pertes. Les embuscades et les retraites en ont fait un combattant dangereux et une voix stable parmi ceux qui apprenaient encore à résister. | Tir +4, Mêlée +3, Social +1 |
| Âge adulte | éclaireur Jaffa libre | Ce Jaffa a voyagé entre les communautés libérées, surveillant les routes, les ruines et les Portes des étoiles à la recherche d'ennemis de retour. Les trajets entre colonies lui ont appris à lire les traces, conduire les animaux de bât et choisir le terrain avant un combat. | Tir +3, Animaux +2, Plantes +2 |
| Âge adulte | artisan Jaffa libre | Ce Jaffa a construit une vie civile après des générations de service imposé, réparant les outils et transmettant des savoir-faire pratiques. Il a réorienté ses anciennes compétences d'entretien militaire vers des foyers, des outils et des structures conçus pour durer. | Artisanat +4, Construction +3 |
| Âge adulte | agriculteur Jaffa libre | Ce Jaffa a cultivé les terres d'un village indépendant et préparé soigneusement la saison suivante. Les années difficiles lui ont appris la patience envers le sol, les animaux et les réserves communes qui séparaient la liberté de la famine. | Plantes +5, Animaux +2, Cuisine +1 |
| Âge adulte | guérisseur Jaffa libre | Ce Jaffa a soigné ses voisins avec des ressources limitées, combinant les connaissances héritées et les remèdes disponibles localement. Il a appris à nettoyer les blessures, identifier les plantes utiles et rassurer les patients effrayés. | Médecine +4, Plantes +2, Intellectuel +1 |
| Âge adulte | ancien déserteur Goa'uld | Ce Jaffa a déserté une force Goa'uld et passé des années à prouver que quitter un ancien maître n'était que la première étape vers la liberté. Une vie sous le soupçon lui a appris à rester vigilant, combattre lorsqu'il était acculé et gagner la confiance par des actes utiles. | Tir +3, Mêlée +2, Social +2 |

### Humains hors-monde — enfances

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Enfance | enfant d'un village tributaire | Élevé dans une colonie humaine isolée sous des souverains lointains, cet enfant a appris que la survie dépendait souvent de la prudence et de la coopération. Le travail partagé entre les familles lui a donné une connaissance pratique des cultures, de la nourriture et de l'humeur de la communauté. | Plantes +2, Social +1, Cuisine +1 |
| Enfance | enfant de serviteurs de palais | Cet enfant a grandi parmi les serviteurs attachés à une grande demeure, apprenant à observer le rang, l'humeur et le danger avant de parler. Il est devenu habile à anticiper les demandes et à dissimuler ses réactions devant des adultes dangereux. | Social +3, Cuisine +1 |
| Enfance | enfant d'un marché hors-monde | Cet enfant a grandi autour d'un marché hors-monde animé où voyageurs, rumeurs et marchandises inconnues arrivaient de colonies lointaines. Les négociations et les coutumes étrangères ont affûté son instinct social et sa curiosité pour le monde extérieur. | Social +3, Intellectuel +1 |
| Enfance | pupille de temple | Élevé près d'un complexe religieux, cet enfant a appris les cérémonies, les tabous et le silence inquiet attendu autour des visiteurs puissants. Les tâches rituelles ont développé sa mémoire, ses manières et son attention aux symboles. | Social +2, Art +1, Intellectuel +1 |
| Enfance | enfant de caravane hors-monde | Cet enfant a voyagé avec une caravane entre des communautés dispersées et appris à s'adapter rapidement lorsque la route changeait. Il a appris à calmer les animaux, juger les étrangers et trouver de la nourriture ou un abri sur des itinéraires incertains. | Animaux +2, Social +2, Plantes +1 |
| Enfance | enfant d'une ferme isolée | Cet enfant a grandi dans une ferme isolée où la météo, les récoltes et les étrangers armés façonnaient le quotidien. L'isolement l'a rendu à l'aise avec le bétail, les cultures et les problèmes qu'il fallait résoudre sans aide extérieure. | Plantes +3, Animaux +2 |

### Hôtes Goa'uld

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Âge adulte | administrateur Goa'uld de domaine | Ce Goa'uld a géré les tributs, le travail et les conflits locaux depuis la sécurité d'une colonie de domaine. Une administration efficace exigeait des registres précis, des ordres persuasifs et une bonne compréhension des rivaux qui pouvaient être achetés. | Social +4, Intellectuel +3 |
| Âge adulte | émissaire Goa'uld de palais | Ce Goa'uld a porté exigences et promesses entre les cours, considérant chaque conversation comme une épreuve d'influence. Il a appris à flatter, menacer et négocier sans révéler le peu de certitude qui soutenait le message de son maître. | Social +5, Intellectuel +2 |
| Âge adulte | gouverneur Goa'uld tributaire | Ce Goa'uld a supervisé une communauté tributaire et appris à obtenir l'obéissance sans provoquer de révolte. Maintenir le tribut exigeait du calcul, une autorité publique et assez d'assurance martiale pour survivre aux troubles. | Social +4, Intellectuel +2, Tir +1 |
| Âge adulte | prêtre d'un culte Goa'uld | Ce Goa'uld a cultivé l'autorité rituelle, encourageant les fidèles à confondre peur, émerveillement et loyauté. Il a maîtrisé la voix, les symboles et les prodiges mis en scène qui transformaient la peur en obéissance. | Social +5, Art +2 |
| Âge adulte | gardien d'archives Goa'uld | Ce Goa'uld a entretenu des archives, des artefacts et des récits remaniés afin de renforcer un domaine. Le classement des anciennes technologies récompensait la patience, la curiosité technique et la capacité à décider quelle vérité devait disparaître. | Intellectuel +5, Art +1, Artisanat +1 |
| Âge adulte | stratège de cour Goa'uld | Ce Goa'uld a conseillé une cour où chaque alliance était temporaire et chaque concession apparente dissimulait un autre calcul. Il a survécu en reliant des fragments de renseignement, en lisant les motivations et en présentant ses conseils comme les propres idées de son souverain. | Intellectuel +4, Social +4 |

### Grands Maîtres Goa'uld

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Âge adulte | Grand Maître Goa'uld conquérant | Ce Grand Maître a bâti un domaine par la conquête, les tributs et une cruauté soigneusement mise en scène. Il a cultivé une présence autoritaire et une violence personnelle, entrant lui-même au combat lorsque le spectacle renforçait sa domination. | Social +4, Tir +3, Mêlée +2 |
| Âge adulte | Grand Maître Goa'uld dynaste | Ce Grand Maître a hérité d'un ancien domaine et préservé son apparence de permanence pendant que ses rivaux éprouvaient chaque frontière. L'autorité de cour, les archives héritées et les intrigues constantes lui ont donné le goût de la persuasion et des calculs à long terme. | Social +5, Intellectuel +3 |
| Âge adulte | Grand Maître Goa'uld intrigant | Ce Grand Maître a préféré la manipulation à la guerre ouverte, employant émissaires, otages et générosité sélective pour affaiblir ses rivaux. Il a accumulé les secrets, cartographié les loyautés et préféré une conversation utile à une bataille coûteuse. | Social +5, Intellectuel +4 |
| Âge adulte | Grand Maître Goa'uld guerrier | Ce Grand Maître a gouverné par le spectacle militaire et attendu de ses commandants Jaffa qu'ils répondent à chaque revers par une campagne plus dure. Il comprenait les armes, l'intimidation et l'art politique de récompenser une victoire avant d'exiger la campagne suivante. | Tir +4, Mêlée +3, Social +3 |

### Tok'ra

| Étape | Nom | Description | Compétences |
|---|---|---|---|
| Âge adulte | agent infiltré Tok'ra | Ce Tok'ra a passé des années derrière les lignes ennemies, cultivant la patience et les habitudes qui rendaient crédible une fausse identité. Maintenir une couverture exigeait une parole persuasive, une observation attentive et assez d'expérience du combat pour s'échapper lorsque la tromperie échouait. | Social +4, Tir +2, Intellectuel +2 |
| Âge adulte | médecin Tok'ra | Ce Tok'ra a soigné des hôtes, des agents et des alliés avec des ressources limitées, arbitrant entre le besoin médical et le secret opérationnel. Sa pratique associait des soins précis à la discrétion nécessaire pour protéger le patient comme la cellule. | Médecine +5, Intellectuel +2, Social +1 |
| Âge adulte | diplomate Tok'ra | Ce Tok'ra a négocié une coopération fragile avec des communautés méfiantes et appris que la confiance se gagnait progressivement. Il a appris à entendre la peur derrière les paroles hostiles et à bâtir des accords capables de survivre à une confiance imparfaite. | Social +5, Intellectuel +2 |
| Âge adulte | éclaireur Tok'ra | Ce Tok'ra a cartographié les routes sûres, les sites abandonnés et les mouvements ennemis, voyageant souvent avec pour seul outil une couverture. Les déplacements lointains en ont fait un tireur compétent, un pisteur et un conducteur capable de s'adapter au moyen de transport exigé par son identité. | Tir +3, Plantes +2, Animaux +2 |
| Âge adulte | analyste du renseignement Tok'ra | Ce Tok'ra a assemblé des rapports dispersés pour produire du renseignement exploitable et appris à se méfier des explications trop simples. Il a comparé les témoignages, la logistique et les habitudes ennemies jusqu'à transformer un motif caché en avertissement utilisable. | Intellectuel +5, Social +2 |
| Âge adulte | messager Tok'ra | Ce Tok'ra a transporté messages et fournitures entre des cellules dissimulées, considérant chaque trajet comme une épreuve de discrétion. Un passage sûr dépendait d'une maîtrise correcte des armes, d'un savoir-faire de terrain adaptable et de la capacité à paraître ordinaire parmi les étrangers. | Tir +2, Animaux +2, Social +2, Plantes +1 |
<!-- BACKSTORY_TABLES_END -->

## Humains Tau'ri

Dans un scénario normal, les starters humains conservent les histoires vanilla compatibles avec une origine terrienne. Les six parcours adultes SGC — forces spéciales, médecine de terrain, recherche, linguistique, ingénierie ou liaison — sont ajoutés au même ensemble pondéré que les carrières vanilla compatibles. Ils apparaissent donc occasionnellement, sans pourcentage de remplacement fixe. Le scénario **Équipe SG isolée** impose toujours l'un de ces six parcours adultes.

## Jaffa

Les Jaffa générés normalement reçoivent des enfances dédiées, puis un parcours adulte différent selon leur faction.

Les serviteurs des Goa'uld peuvent devenir guerriers de domaine, gardes de temple, vétérans, instructeurs, officiers ou sentinelles de Chappa'ai.

Les Jaffa libres peuvent devenir combattants affranchis, protecteurs, éclaireurs, artisans, agriculteurs, guérisseurs ou anciens déserteurs.

## Hôtes Goa'uld

Les Goa'uld ordinaires et les Grands Maîtres utilisent une enfance humaine hors-monde et des carrières adaptées à leur caste. Les administrateurs, prêtres, stratèges et souverains se distinguent désormais aussi par leurs compétences sociales, intellectuelles ou militaires.

## Tok'ra

Les agents Tok'ra générés reçoivent des parcours d'infiltrateur, médecin, diplomate, éclaireur, analyste ou messager.

Un colon existant qui accepte volontairement un symbiote Tok'ra conserve son histoire passée.

## Création manuelle et compatibilité

Les filtres garantissent une génération cohérente par défaut. Un outil externe de création de personnages peut toujours imposer une combinaison inhabituelle pour un scénario personnalisé.

Les identifiants, catégories et silhouettes des histoires existantes sont conservés. Les anciennes sauvegardes ne nécessitent aucune migration et aucun personnage n'est renommé ou régénéré.

Le nombre d'histoires n'augmente pas dans cette version. Une extension raisonnable pourra être étudiée plus tard. Les futures cultures, notamment Asgard, Nox et Unas, recevront leurs propres noms et histoires lors de leur intégration ou dans un jalon immédiatement suivant.
