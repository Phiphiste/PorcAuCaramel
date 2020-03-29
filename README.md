# PorcAuCaramel by Arnaud Emprin, Philippe Rambaud & Jean Leroy

## Thème
Malgré un titre culinaire appétissant,notre application permet la recherche et l'affichage
non pas de cuisine asiatique mais biende jouers jouant ou ayant joué dans la ligue 
américaine de basketall, la NBA. 

## Requête
Nous effectuons la requête sur wikidata, ce qui explique que la liste des joueurs que
nous recevons variable. De plus, chaque joueur ayant joué dans plusieurs équipes, et 
parfois à plusieurs positions, la liste brute retournée par la requête est beaucoup
plus longue que la liste finale de joueur. Nous arrivons donc à récupérer au total 
autour de 4000 joueurs et l'opération peut parfois prendre jusqu'à une minute.

## Fonctionnement 
La 1ère fenêtre permet juste de lancer ladite requête.
La fenêtre principale est composée d'une bannière de filtres et d'une douzaine de 
résultats par page en dessous. Les filtres concernent l'identité des joueurs,
ses caractéristiques physiques, son poste de jeu, ses équipes et son palmarès 
individuel.
Pour afficher la carte Joueur et accéder à plus de détails, il suffit de double 
cliquer sur le nom du joueur dans la liste des résultats.

## Difficultés rencontrées
### Absence de certains champs cruciaux sur Wikidata
WikiData est un outil puissant mais malheuresement limité sur certains points. Dans notre
cas, il nous a été impossible de récupérer des éléments de palmarès, de récompenses
individuelles et d'équipe. C'est pourquoi nous avons du ajouter au projet quelques fichiers
csv pour alimenter notre base sur ces points cruciaux (AllStars,MVP,DPOY,NBA Teams etc.)
### Unités de mesure
Les éléments "Taille" et "Poids", entre autres, sont parfois exprimés en unités américaines,
parfois en unités internationale. Cela peut mener à des incohérences puisque nous avons
fait le choix de tout exprimer en kg et cm.
### Test unitaires
Nous avons créé et implémenté un test unitaire à votre demande sur le framework MSTest.
Cependant, pour une raison qui nous échappe, le test n'est pas reconnu par le projet et 
ne se lance jamais. Vous pouvez cependant le retrouver dans la solution.
### Méthodes asynchrones
Nous avions à coeur de concevoir des méthodes asynchrones pour donner un feedback visuel
lors du chargement de la requête et de l'affichage de la photo sur la carte joueur. Cependant,
à cause de quelques difficultés techniques et des conditions actuelles exceptionnelles,
nous ne proposons pas de telles méthodes dans notre projet.
