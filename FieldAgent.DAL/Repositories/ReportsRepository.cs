using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IConfigurationRoot Config;
        string connectionString;
        private readonly FactoryMode mode;

        public ReportsRepository(IConfigurationRoot config)
        {
            Config = config;
            string environment = mode == FactoryMode.TEST ? "Test" : "Prod";
            connectionString = Config[$"ConnectionStrings:{environment}"];
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            Response<List<TopAgentListItem>> response = new Response<List<TopAgentListItem>>();
            response.Data = new List<TopAgentListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("TopAgents", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.Data.Add(new TopAgentListItem
                            {
                                NameLastFirst = reader["Name"].ToString(),
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                CompletedMissionCount = (int)reader["NumberOfMissions"]
                            });
                            response.Success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Success = false;
                }
            }

            return response;
        }
        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            Response<List<PensionListItem>> response = new Response<List<PensionListItem>>();
            response.Data = new List<PensionListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("PensionList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@agencyId", agencyId);

                try
                {
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.Data.Add(new PensionListItem
                            {
                                AgencyName = reader["ShortName"].ToString(),
                                BadgeId = (Guid)reader["BadgeId"],
                                NameLastFirst = reader["NameLastFirst"].ToString(),
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                DeactivationDate = (DateTime)reader["DeactivationDate"],
                            });
                            response.Success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Success = false;
                }
            }

            return response;
        }

        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            Response<List<ClearanceAuditListItem>> response = new Response<List<ClearanceAuditListItem>>();
            response.Data = new List<ClearanceAuditListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ClearanceAudit", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@securityClearanceId", securityClearanceId);
                cmd.Parameters.AddWithValue("@agencyId", agencyId);

                try
                {
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new ClearanceAuditListItem();

                            row.BadgeId = (Guid)reader["BadgeId"];
                            row.NameLastFirst = reader["NameLastFirst"].ToString();
                            row.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            row.ActivationDate = (DateTime)reader["ActivationDate"];

                            if (reader["DeactivationDate"] != DBNull.Value)
                            {
                                row.DeactivationDate = (DateTime)reader["DeactivationDate"];
                            }

                            response.Data.Add(row);
                        }
                        response.Success = true;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Success = false;
                }
            }

            return response;
        }
    }
}
