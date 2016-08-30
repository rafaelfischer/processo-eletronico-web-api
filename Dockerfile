FROM microsoft/dotnet:latest

COPY ProcessoEletronicoWebAPI/src /home/src/
WORKDIR /home/src/WebAPI.Publico

RUN dotnet restore

EXPOSE 3309/tcp

CMD ["dotnet", "run"]
