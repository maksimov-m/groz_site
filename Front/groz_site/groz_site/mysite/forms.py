from django.contrib.auth.forms import  AuthenticationForm
from django import forms


class UserLoginForm(AuthenticationForm):
    username = forms.CharField(label='Логин', widget=forms.TextInput(
        attrs={'class': 'form-control'}
    ))
    password = forms.CharField(label='Пароль', widget=forms.PasswordInput(
        attrs={'class': 'form-control'}
    ))

class CourseForm(forms.Form):
    title = forms.CharField(label='Название курса', max_length=100)
    description = forms.CharField(label='Описание курса', widget=forms.Textarea)

class AssignmentForm(forms.Form):
    title = forms.CharField(label='Название', max_length=100)
    description = forms.CharField(label='Описание', widget=forms.Textarea)
    photo = forms.ImageField(label='Прикрепление фотографии', required=False)