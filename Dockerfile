FROM microsoft/dotnet:1.1-sdk-projectjson

COPY ProcessoEletronicoWebAPI/src /home/src/
WORKDIR /home/src/WebAPI

RUN dotnet restore

EXPOSE 3308/tcp

CMD ["dotnet", "run"]
