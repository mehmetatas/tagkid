app.controller('NoteCtrl', ['$scope', '$http', function($scope, $http) {

  $scope.notes= [
       {
	     "content": "AngularJS - HTML enhanced for web apps!",
	     "color": "warning",
	 	 "date": 1410788931159
	   },
	   {
	     "content": "Meeting",
	     "color": "primary",
	     "date": 1410788631159
	   },
	   {
	     "content": "Keep watching",
	     "color": "success",
	     "date": 1410788731159
	   }
	 ];
  $scope.note = $scope.notes[0];
  $scope.notes[0].selected = true;

  $scope.colors = ['primary', 'info', 'success', 'warning', 'danger', 'dark'];

  $scope.createNote = function(){
    var note = {
      content: 'New note',
      color: $scope.colors[Math.floor((Math.random()*$scope.colors.length))],
      date: Date.now()
    };
    $scope.notes.push(note);
    $scope.selectNote(note);
  }

  $scope.deleteNote = function(note){
    $scope.notes.splice($scope.notes.indexOf(note), 1);
    if(note.selected){
      $scope.note = $scope.notes[0];
      $scope.notes.length && ($scope.notes[0].selected = true);
    }
  }

  $scope.selectNote = function(note){
    angular.forEach($scope.notes, function(note) {
      note.selected = false;
    });
    $scope.note = note;
    $scope.note.selected = true;
  }

}]);