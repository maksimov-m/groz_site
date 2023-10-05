import json
import requests
from django.shortcuts import render, get_object_or_404, redirect
from django.views.decorators.csrf import csrf_exempt

from .forms import UserLoginForm, AssignmentForm, CourseForm


# Create your views here.

@csrf_exempt
def index(request):
    if request.method == 'POST':
        print("1!!!!")
    return render(request, 'mysite/index.html')


def user_login(request):
    if request.method == 'POST':
        form = UserLoginForm(data=request.POST)
        print(form.data['username'], form.data['password'])
        url = 'http://192.168.0.42:61752/api/v1/login'
        myobj = {"login": "A2", "password": "A2"}

        x = requests.post(url, json=myobj)
        print(x.text, type(x.text))
        if form.data['username'] == "student":
            return redirect('user_main')
        else:
            return redirect('teacher_main')
    else:
        form = UserLoginForm()
    return render(request, 'mysite/login.html', {'form': form})


def user_main(request):
    return render(request, 'mysite/user_main.html')


def teacher_main(request):
    return render(request, 'mysite/teacher_main.html')

def user_lk(request):
    return render(request, 'mysite/user_lk.html')

def create_course(request):
    if request.method == 'POST':
        form = CourseForm(request.POST)
        if form.is_valid():
            # Обработка данных формы, например, сохранение в базу данных
            return render(request, 'mysite/course_created.html', {'title': form.cleaned_data['title']})
    else:
        form = CourseForm()

    return render(request, 'mysite/create_course.html', {'form': form})


def add_assignment(request):
    if request.method == 'POST':
        form = AssignmentForm(request.POST, request.FILES)
        if form.is_valid():
            title = form.cleaned_data['title']
            description = form.cleaned_data['description']
            photo = form.cleaned_data['photo']

            # Обработка и сохранение данных в базу данных или другую логику

            return redirect('assignments_list')  # Перенаправляем на страницу со списком заданий
    else:
        form = AssignmentForm()

    return render(request, 'mysite/add_assignment.html', {'form': form})


def kourse_list(request):
    tasks = [{"title": "Россия. Города.", "description": "Курс по Российским городам"},
             {"title": "Россия. Реки.", "description": "Курс по Российским рекам"}]
    return render(request, 'mysite/kourse.html', {'tasks': tasks})


def students_list(request):
    students = [{"fio": "Сергеев Сергей Махитович", "class": "2"},
                {"fio": "Алабаев Рустем Паратович", "class": "2"},
                {"fio": "Малитова Равиля Руслановна", "class": "3"},
                {"fio": "Папараев Рустем Вахитович", "class": "3"}]
    return render(request, 'mysite/students_list.html', {'students': students})
