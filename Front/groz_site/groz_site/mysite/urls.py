from django.urls import path
from .views import *

urlpatterns = [
    #path('', index, name='index'),
    path('', user_login, name='login'),
    path('user_main/', user_main, name='user_main'),
    path('teacher_main/', teacher_main, name='teacher_main'),
    path('add_assignment/', add_assignment, name='add_assignment'),
    path('create_course/', create_course, name='create_course'),
    path('students/', students_list, name='students'),
    path('user_lk/', user_lk, name='user_lk'),
    path('kourse/', kourse_list, name='kourse'),
]
